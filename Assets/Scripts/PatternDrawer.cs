using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
 

public class PatternDrawer : MonoBehaviour
{
    [Header("Setup")]
    public List<DrawSlot> slots = new();
    public LineRenderer linePrefab;
    public DrawStartArea drawStartArea;
    public DrawGemsSpawner drawGemSpawner;
    public Gradient commonLineColor;
    public Gradient highlightLineColor;
    [Header("DEBUG")]
    public bool drawing;
    public LineRenderer lastLine;
    public DrawSlot lastSlot;
    public List<Pattern> drawPatterns = new();
    public List<Pattern> lastPatterns = new();
    public List<LineRenderer> linesInPieces = new();
    Camera _cam;
    public event Action<List<Pattern>> OnRelease = delegate { };

    public void HighlightLines()
    {
        foreach (var line in linesInPieces)
        {
            line.colorGradient = highlightLineColor;
        }
    }
    public void CommonLines()
    {
        foreach (var line in linesInPieces)
        {
            line.colorGradient = commonLineColor;
        }
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
        drawing = true;
    }

    void OnSlotSelected(DrawSlot slot)
    {
        // if (!drawing) return;
        drawing = true;
        if (!slot.Selected)
        {
            drawGemSpawner.Spawn(slot);
            slot.Select();
        }
 
        if (!lastLine)
        {
            lastSlot = slot;
            lastLine = Instantiate(linePrefab);
            lastLine.colorGradient = commonLineColor;
            lastLine.positionCount = 2;
            var pos = GetLinePos(slot.transform.position);
            lastLine.SetPosition(0, pos);
            lastLine.SetPosition(1, pos);
        }
        else
        {
            if (lastSlot != slot)
            {
                var newPattern = new Pattern(lastSlot, slot);
                if (!AnySame(newPattern))
                {
                    drawPatterns.Add(newPattern);
                    linesInPieces.Add(lastLine);
                    var pos = GetLinePos(newPattern.From.transform.position);
                    var pos2 = GetLinePos(newPattern.To.transform.position);
                    lastLine.SetPosition(0, pos);
                    lastLine.SetPosition(1, pos2);

                    lastLine = Instantiate(linePrefab);
                    lastLine.colorGradient = commonLineColor;
                    lastLine.positionCount = 2;
                    var pos3 = GetLinePos(slot.transform.position);
                    lastLine.SetPosition(0, pos3);
                    lastLine.SetPosition(1, pos3);
                    lastSlot = slot;
                }
            }
        }
    }

    bool AnySame(Pattern check)
        => drawPatterns.Any(p =>
            p.start == check.start && p.end == check.end ||
            p.start == check.end && p.end == check.start);

    static Vector3 GetLinePos(Vector3 pos)
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
        foreach (var line in linesInPieces.Where(line => line))
        {
            Destroy(line.gameObject);
        }

        linesInPieces = new List<LineRenderer>();
        drawPatterns = new List<Pattern>();
        if (lastLine) Destroy(lastLine.gameObject);
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
            drawing = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            drawing = false;
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
            var pos = GetLinePos(_cam.ScreenToWorldPoint(Input.mousePosition));
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