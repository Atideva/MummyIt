using UnityEngine;

[System.Serializable]
// ReSharper disable InconsistentNaming
public class Pattern
{
    public int start;
    public int end;
    [HideInInspector] public DrawSlot From;
    [HideInInspector] public DrawSlot To;

    public Pattern(DrawSlot from, DrawSlot to)
    {
        start = from.id;
        end = to.id;
        From = from;
        To = to;
    }
}