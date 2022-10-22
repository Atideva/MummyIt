using System.Collections.Generic;
using UnityEngine;

public class PlateArmorListUI : MonoBehaviour
{
    [SerializeField] GameObject container;
    [SerializeField] RectTransform gemPosUP;
    [SerializeField] List<PlateArmorUI> platesUI = new();

    public void DisableAll()
    {
        container.SetActive(false);
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
        gemPosUP.anchoredPosition = new Vector2(0, 35);
        Debug.LogWarning("Shit here! Moving gems ui, cause armor enabled");
        container.SetActive(true);
        if (id >= platesUI.Count) return;
        platesUI[id].Enable();
    }

    public void Disable(int id)
    {
        if (id >= platesUI.Count) return;
        platesUI[id].Disable();
    }
}