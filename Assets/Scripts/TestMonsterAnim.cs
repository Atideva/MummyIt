using UnityEngine;

// ReSharper disable InconsistentNaming

public class TestMonsterAnim : MonoBehaviour
{
    public Animator anim;
    static readonly int attack = Animator.StringToHash("attack");
    public float attackDur = 0.5f;

    public void Attack()
    {
        if (anim)
            anim.SetTrigger(attack);
    }
}