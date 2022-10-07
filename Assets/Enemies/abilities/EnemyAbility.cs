using UnityEngine;

public abstract class EnemyAbility : MonoBehaviour
{
    public Enemy Owner { get; set; }

    public void Init(Enemy enemy)
    {
        Owner = enemy;
        OnInit();
    }

    protected abstract void OnInit();
    public abstract void Reset();
}