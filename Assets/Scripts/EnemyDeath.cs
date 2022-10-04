using DG.Tweening;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [Header("Melee Punch anim")]
    public float punchDistance;
    public float punchTime;
    public float punchRotate;

    [Header("TEST")]
    public bool test;
    public Enemy testEnemy;
    public Vector3 testPos;

#if UNITY_EDITOR
    void Update()
    {
        if (test)
        {
            test = false;
            PunchAnim(testEnemy);
        }
    }
#endif

    void Start()
    {
        if (testEnemy) testPos = testEnemy.transform.position;
        Events.Instance.OnEnemyDeath += OnEnemyDeath;
    }

    void OnEnemyDeath(Enemy enemy)
    {
        if (enemy.LastTakenDmgIsMelee)
            PunchAnim(enemy);
        else
            DeathAnim(enemy);
    }

    void PunchAnim(Enemy enemy)
    {
        var t = enemy.transform;
        var rot = new Vector3(0, 0, punchRotate);
        var newPos = t.position + Vector3.up * punchDistance;
        t.DORotate(rot, punchTime).SetRelative(true);
        t.DOMove(newPos, punchTime).OnComplete(()
            => Disable(enemy));
    }

    void DeathAnim(Enemy enemy)
    {
        //play death anim
        enemy.ReturnToPool();
    }

    void Disable(Enemy enemy)
    {
        if (enemy == testEnemy)
            enemy.transform.position = testPos;
        enemy.ReturnToPool();
    }
}