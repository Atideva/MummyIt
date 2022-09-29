using System.Collections.Generic;
using UnityEngine;

public class PatternUI : MonoBehaviour
{
    public List<PatterPointUI> points = new();
    public List<PatternLine> lines = new();

    public void Disable()
    {
        foreach (var line in lines)
            line.Stop();
        lines = new List<PatternLine>();
    }

    public void Set(IReadOnlyList<Pattern> patterns)
    {
        if (patterns.Count == 0) return;

        foreach (var ui in points)
            ui.Disable();

        foreach (var p in patterns)
        {
            EnablePoint(p.start);
            EnablePoint(p.end);
            CreateLine(p.start, p.end);
        }
    }

    void CreateLine(int start, int end)
    {
        var s = start - 1;
        var e = end - 1;
        if (s < 0 || s >= points.Count ||
            e < 0 || e >= points.Count) return;
        var line = Lines.Instance.Get();
        line.Follow(points[s].transform, points[e].transform);
        lines.Add(line);
    }

    void EnablePoint(int number)
    {
        var id = number - 1;
        if (id >= 0 && id < points.Count)
        {
            points[id].Enable();
        }
        else
        {
            Debug.LogError("Pattern circle ID is out of range!");
        }
    }
}