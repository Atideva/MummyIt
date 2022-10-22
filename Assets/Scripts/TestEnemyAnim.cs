using System.Collections.Generic;
using UnityEngine;

public class TestEnemyAnim : MonoBehaviour
{
    public List<EnemyConfig> enemies = new();
    public Enemy created;
    public EnemyDeath enemyDeaths;

    int _id;

    void Start()
    {
        _id = -1;
        enemyDeaths.NoPoolMode();
    }


    [EButton.BeginHorizontal("TEST"), EButton.BeginVertical("Create"), EButton]
    void Next()
    {
        if (created)
            Destroy(created.gameObject);

        _id++;
        if (_id >= enemies.Count)
            _id = 0;

        Create();
    }

    [EButton]
    void Back()
    {
        if (created)
            Destroy(created.gameObject);

        _id--;
        if (_id < 0)
            _id = enemies.Count - 1;

        Create();
    }

    [EButton, EButton.EndVertical]
    void Kill()
    {
        created.Damage(1000000);
        created = null;
    }

    [EButton.BeginVertical("Animations"), EButton]
    void Attack()
    {
        created.animator.Attack();
    }

    [EButton]
    void TakeDamage()
    {
        created.animator.TakeDamage();
    }


    void Create()
    {
        created = Instantiate(enemies[_id].prefab, transform);
        created.transform.localPosition = Vector3.zero;
        created.SetConfig(enemies[_id]);
    }


    /*
     
         // [Header("Create")]
    // public bool current;
    // public bool next;
    // public bool previous;
    //
    // [Header("Animations")]
    //   public bool move;
    //public bool attack;
//    public bool death;
    //  public bool idle;
//    public bool takeDamage;
    //  [Header("Kill")]
    //   public bool kill;
    // void Update()
    // {
    //     if (current)
    //     {
    //         current = false;
    //         if (!created)
    //             Create();
    //     }
    //
    //     if (next)
    //     {
    //         next = false;
    //         if (created)
    //             Destroy(created.gameObject);
    //
    //         id++;
    //         if (id >= enemies.Count)
    //             id = 0;
    //
    //         Create();
    //     }
    //
    //     if (previous)
    //     {
    //         previous = false;
    //         if (created)
    //             Destroy(created.gameObject);
    //
    //         id--;
    //         if (id < 0)
    //             id = enemies.Count - 1;
    //
    //         Create();
    //     }
    //
    //     if (!created) return;
    //
    //     // if (move)
    //     // {
    //     //     move = false;
    //     //     created.animator.Move();
    //     // }
    //
    //     // if (attack)
    //     // {
    //     //     attack = false;
    //     //     created.animator.Attack();
    //     // }
    //     //
    //     // if (death)
    //     // {
    //     //     death = false;
    //     //     created.animator.Death();
    //     // }
    //     //
    //     // if (idle)
    //     // {
    //     //     idle = false;
    //     //     created.animator.Idle();
    //     // }
    //     //
    //     // if (takeDamage)
    //     // {
    //     //     takeDamage = false;
    //     //     created.animator.TakeDamage();
    //     // }
    //     //
    //     // if (kill)
    //     // {
    //     //     kill = false;
    //     //     created.Damage(1000000);
    //     //     created = null;
    //     // }
    // }
    
    */
}