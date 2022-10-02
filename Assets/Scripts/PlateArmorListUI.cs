using System.Collections.Generic;
using UnityEngine;

public class PlateArmorListUI : MonoBehaviour
{
    [SerializeField] List<PlateArmorUI> platesUI = new();

    public void DisableAll()
    {
        foreach (var ui in platesUI)
            ui.Disable();
    }

    public void Refresh(int id, float value)
    {
        if (id >= platesUI.Count) return;
        platesUI[id].Set(value);
    }

    public void Enable(int id)
    {
        if (id >= platesUI.Count) return;
        platesUI[id].Enable();
    }

    public void Disable(int id)
    {
        if (id >= platesUI.Count) return;
        platesUI[id].Disable();
    }
}