using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public int id;
    public TextMeshProUGUI idTxt;
    public event Action<Slot> OnSelected = delegate { };

    void Awake()
        => idTxt.enabled = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnSelected(this);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnSelected(this);
    }
    
}