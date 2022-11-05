using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DrawSlot : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public int id;
    public TextMeshProUGUI idTxt;
    //  public Image img;
    // public Image activeImg;

    public event Action<DrawSlot> OnSelected = delegate { };

    void Awake()
        => idTxt.enabled = false;

    public bool Active { get; private set; }
    public bool NotActive => !Active;

    public void Activate()
    {
        Active = true;
        //  activeImg.enabled = true;
    }

    public void Release()
    {
        Active = false;
        //  activeImg.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnSelected(this);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnSelected(this);
    }
}