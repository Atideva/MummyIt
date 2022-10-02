using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GunAnimation : MonoBehaviour
{
    [Header("Setup")]
    public DOTweenAnimation rotate;
    [Header("TEST")]
    public bool testRotate;

    void Start()
    {
    }

    public void Rotate()
    {
        rotate.DORestart();
        rotate.DOPlay();
    }


#if UNITY_EDITOR
    void Update()
    {
        if (testRotate)
        {
            testRotate = false;
            Rotate();
        }
    }
#endif
}