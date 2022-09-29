using System.Collections.Generic;
using UnityEngine;

public class TestPatternView : MonoBehaviour
{
    public List<TestPatternPoint> points = new();

    public void Set(List<Pattern> patterns)
    {
        if (patterns.Count == 0) return;
        var pattern = patterns[0];
        foreach (var point in points)
            point.Disable();

        points[pattern.start-1].Enable();
        points[pattern.end-1].Enable();
    }
}