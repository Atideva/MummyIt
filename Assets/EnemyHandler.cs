using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public EnemySpawner spawner;
    public Transform attackPos;
    float _attackY;

    void Start()
    {
        _attackY = attackPos.transform.position.y;
        spawner.OnSpawn += OnSpawn;
    }

    void OnSpawn(Enemy enemy)
    {
        enemy.Move();
    }

    void FixedUpdate()
    {
        foreach (var enemy in spawner.currentEnemies)
        {
            if (enemy.transform.position.y <= _attackY)
            {
                if (enemy.IsMove) enemy.StopMove();
                if (!enemy.IsMeleeAttack) enemy.StartMeleeAttack();
            }
            else
            {
                if (!enemy.IsMove) enemy.Move();
                if (enemy.IsMeleeAttack) enemy.StopMeleeAttack();
            }
        }
    }
}