using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

 
public class Slot : MonoBehaviour, IPointerEnterHandler
{
    public int id;
    public event Action<Slot> OnSelected=delegate {  };
    public TextMeshProUGUI idTxt;

    void Awake()
        => idTxt.enabled = false;

    public void OnPointerEnter(PointerEventData eventData)
        => OnSelected(this);
}
