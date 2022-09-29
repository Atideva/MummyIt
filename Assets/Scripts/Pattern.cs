using UnityEngine;

[System.Serializable]
// ReSharper disable InconsistentNaming
public class Pattern
{
    public int start;
    public int end;
    [HideInInspector] public Slot From;
    [HideInInspector] public Slot To;

    public Pattern(Slot from, Slot to)
    {
        start = from.id;
        end = to.id;
        From = from;
        To = to;
    }
}