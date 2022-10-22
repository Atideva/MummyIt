using System;
using EPOOutline;
using UnityEngine;

public class GunView : MonoBehaviour
{
    [SerializeField] Transform firePos;
    [SerializeField] Transform origin;
    [SerializeField] SpriteRenderer aim ;
    [SerializeField] Outlinable outline;
 
    public Vector3 FirePos => firePos.position;
    public Quaternion Rotation => firePos.rotation;

    public Vector3 OriginPos => origin.position;

    void Awake()
    {
        aim.enabled = false;
        DisableOutline();
    }

    public void TakeAim()
    {
        aim.enabled = true;
    }

    public void EnableOutline()
    {
        outline.enabled = true;
    }
    
    public void DisableOutline()
    {
        outline.enabled = false; 
    }
}
