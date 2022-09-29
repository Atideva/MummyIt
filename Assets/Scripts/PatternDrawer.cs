using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PatternDrawer : MonoBehaviour
{
    [Header("Setup")]
    public List<Slot> slots = new();
    public LineRenderer linePrefab;

    [Header("DEBUG")]
    public bool drawing;
    public LineRenderer lastLine;
    public Slot lastSlot;
    public List<Pattern> drawPatterns = new();
    public List<Pattern> lastPatterns = new();
    public List<LineRenderer> linesInPieces = new();
    Camera _cam;
    public event Action<List<Pattern>> OnRelease = delegate { };

    void Start()
    {
        _cam = Camera.main;
        for (var i = 0; i < slots.Count; i++)
        {
            slots[i].id = i + 1;
            slots[i].OnSelected += OnSlotSelected;
        }
    }


    void OnSlotSelected(Slot slot)
    {
        if (!drawing) return;

        if (!lastLine)
        {
            lastSlot = slot;
            lastLine = Instantiate(linePrefab);
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
                if (!AnySamePattern(newPattern))
                {
                    drawPatterns.Add(newPattern);
                    linesInPieces.Add(lastLine);
                    var pos = GetLinePos(newPattern.From.transform.position);
                    var pos2 = GetLinePos(newPattern.To.transform.position);
                    lastLine.SetPosition(0, pos);
                    lastLine.SetPosition(1, pos2);

                    lastLine = Instantiate(linePrefab);
                    lastLine.positionCount = 2;
                    var pos3 = GetLinePos(slot.transform.position);
                    lastLine.SetPosition(0, pos3);
                    lastLine.SetPosition(1, pos3);
                    lastSlot = slot;
                }
            }
        }
    }

    bool AnySamePattern(Pattern check)
        => drawPatterns.Any(p =>
            p.start == check.start && p.end == check.end ||
            p.start == check.end && p.end == check.start);

    static Vector3 GetLinePos(Vector3 pos)
        => new(pos.x, pos.y, 0);


    void Release()
    {
        lastPatterns = drawPatterns;
        OnRelease(lastPatterns);
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

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            drawing = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            drawing = false;
            Release();
            Clear();
        }

        if (lastLine)
        {
            var pos = GetLinePos(_cam.ScreenToWorldPoint(Input.mousePosition));
            lastLine.SetPosition(1, pos);
        }
    }
}