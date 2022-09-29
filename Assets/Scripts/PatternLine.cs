using System;
using Pools;
using UnityEngine;

public class PatternLine : PoolObject
{
    public LineRenderer line;
    public Transform t0;
    public Transform t1;
    Camera _cam;

    void Awake()
    {
        _cam = Camera.main;
    }

    public void Follow(Transform targetStart, Transform targetEnd)
    {
        t0 = targetStart;
        t1 = targetEnd;
        line.positionCount = 2;
        RefreshPositions();
    }

    public void Stop()
    {
        t0 = null;
        t1 = null;
        ReturnToPool();
    }

    void FixedUpdate()
    {
        if (!t0 || !t1) return;
        RefreshPositions();
    }

    void RefreshPositions()
    {
        //  var p0 = _cam.ScreenToWorldPoint(t0.transform.position);
        //  var p1 = _cam.ScreenToWorldPoint(t1.transform.position);
        line.SetPosition(0, t0.transform.position);
        line.SetPosition(1, t1.transform.position);
    }
}