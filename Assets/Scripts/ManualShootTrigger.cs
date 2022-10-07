using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ManualShootTrigger : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public event Action OnPointUp = delegate { };
    public event Action OnPointDown = delegate { };

    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointUp();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointDown();
    }
}