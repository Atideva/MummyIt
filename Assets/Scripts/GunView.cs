using System;
using EPOOutline;
using UnityEngine;

public class GunView : MonoBehaviour
{
    [SerializeField] Transform firePos;
    [SerializeField] SpriteRenderer aim ;
    [SerializeField] Outlinable outline;
 
    public Vector3 FirePos => firePos.position;
    public Quaternion Rotation => firePos.rotation;
    void Awake()
    {
        aim.enabled = false;
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
