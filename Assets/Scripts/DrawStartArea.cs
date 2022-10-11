using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DrawStartArea : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public Image trigger;
    public event Action  OnDrawStart=delegate {  };
    public void OnPointerEnter(PointerEventData eventData)
    {
        trigger.enabled = false;
        OnDrawStart();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        trigger.enabled = false;
        OnDrawStart();
    }

    public void Release()
    {
        trigger.enabled = true;
    }
    
}
