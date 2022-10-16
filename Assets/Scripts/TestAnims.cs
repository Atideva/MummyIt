using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnims : MonoBehaviour
{
    public Animator[] animators;
 
    void Start()
    {
        animators = GetComponentsInChildren<Animator>();
        foreach (var anim in animators)
        {
            anim.SetBool("walk",true);
        }

    }

     
}
