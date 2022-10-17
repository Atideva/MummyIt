using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable InconsistentNaming

public class TestMonsterAnim : MonoBehaviour
{
    public Animator anim;
    static readonly int attack = Animator.StringToHash("attack02");
    public float attackDur = 0.5f;

    void Start()
    {
        StartCoroutine(FUCKSHITTHSI());
    }

    IEnumerator FUCKSHITTHSI()
    {
        while (true)
        {
            Attack();
            yield return new WaitForSeconds(1);
        }
    }
    public void Attack()
    {
        if (anim)
            anim.SetTrigger(attack);
    }
}