using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLine : MonoBehaviour
{
    [SerializeField] Transform[] positions;
    public Color gizmoColor;
    public float gizmoSize;

    public Transform[] Positions => positions;


    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        foreach (var VARIABLE in positions)
        {
            Gizmos.DrawCube(VARIABLE.position, Vector3.one * gizmoSize);
        }
    }
}