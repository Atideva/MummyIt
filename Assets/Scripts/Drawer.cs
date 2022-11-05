using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Drawer : MonoBehaviour
{
    [Header("Setup")]
    public List<DrawSlot> slots = new();
    public LineRenderer linePrefab;
    public DrawStartArea drawStartArea;
    public DrawGems drawGem;
    public Gradient commonLineColor;
    public Gradient highlightLineColor;

    [Header("DEBUG")]
    public DrawSlot lastSlot;
    [field: SerializeField] public bool Drawing { get; private set; }

    public LineRenderer lastLine;

    public List<Pattern> drawPatterns = new();
    public List<Pattern> lastPatterns = new();
    public List<LineRenderer> drawLines = new();

    Camera _cam;
    public event Action<List<Pattern>> OnRelease = delegate { };
    public event Action OnNewDraw = delegate { };
    public event Action<List<Pattern>> OnPatternChange = delegate { };


    public void HighlightLines()
    {
        foreach (var line in drawLines)
            line.colorGradient = highlightLineColor;
    }

    public void ПотушитьLines()
    {
        foreach (var line in drawLines)
            line.colorGradient = commonLineColor;
    }

    void Start()
    {
        drawStartArea.OnDrawStart += OnDrawStart;
        _cam = Camera.main;

        for (var i = 0; i < slots.Count; i++)
        {
            slots[i].id = i + 1;
            slots[i].Release();
            slots[i].OnSelected += OnSlotSelected;
        }
    }

    void OnDrawStart()
    {
        Drawing = true;
    }

    void OnSlotSelected(DrawSlot selected)
    {
        if (selected.NotActive)
        {
            drawGem.Spawn(selected);
            selected.Activate();
        }

        if (!Drawing)
        {
            Drawing = true;
            lastSlot = selected;
            lastLine = NewLineFrom(selected);
            OnNewDraw();
            return;
        }

        if (lastSlot == selected) return;

        var draw = new Pattern(lastSlot, selected);
        if (AnySame(draw)) return;

        drawPatterns.Add(draw);
        drawLines.Add(lastLine);

        Bind(lastLine, lastSlot, selected);
        lastLine = NewLineFrom(selected);

        lastSlot = selected;
        OnPatternChange(drawPatterns);
    }

    LineRenderer NewLineFrom(DrawSlot slot)
    {
        var newLine = Instantiate(linePrefab);
        newLine.colorGradient = commonLineColor;
        newLine.positionCount = 2;
        newLine.SetPosition(0, GetPos(slot));
        return newLine;
    }


    void Bind(LineRenderer line, DrawSlot from, DrawSlot to)
    {
        line.SetPosition(0, GetPos(from));
        line.SetPosition(1, GetPos(to));
    }

    bool NoSame(Pattern check) => !AnySame(check);

    bool AnySame(Pattern check) =>
        drawPatterns.Any(p =>
            p.start == check.start && p.end == check.end ||
            p.start == check.end && p.end == check.start);

    static Vector3 GetPos(DrawSlot slot)
        => GetPos(slot.transform.position);

    static Vector3 GetPos(Vector3 pos)
        => new(pos.x, pos.y, 0);

    void Release()
    {
        foreach (var slot in slots)
        {
            slot.Release();
        }

        lastPatterns = drawPatterns;
        OnRelease(drawPatterns);
    }

    void Clear()
    {
        foreach (var line in drawLines.Where(line => line))
        {
            Destroy(line.gameObject);
        }

        drawLines = new List<LineRenderer>();
        drawPatterns = new List<Pattern>();
        if (lastLine)
            Destroy(lastLine.gameObject);
        lastLine = null;
    }

    // bool touched;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            //touched = true;
        }

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Drawing = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Drawing = false;
            Release();
            Clear();
        }
#else
        if (Input.touchCount == 0)
        {
            drawing = false;
            Release();
            Clear();
        }
#endif

        if (lastLine)
        {
            var pos = GetPos(_cam.ScreenToWorldPoint(Input.mousePosition));
            lastLine.SetPosition(1, pos);
        }

        // if (Input.touchCount > 0)
        // {
        //     var i = 0;
        //     while (i < Input.touchCount)
        //     {
        //         var t = Input.GetTouch(i);
        //         var x = t.position.x;
        //         var y = t.position.y;
        //         if (t.phase == TouchPhase.Began)
        //         {
        //             //Left joystick AREA
        //             if (x < Screen.width * 1 / 3 &&
        //                 y < Screen.height * 1 / 3)
        //             {
        //             }
        //             //Right joystick AREA
        //             else if (x > Screen.width * 2 / 3 &&
        //                      y < Screen.height * 1 / 3)
        //             {
        //             }
        //         }
        //
        //         ++i;
        //     }
        // }
    }
}