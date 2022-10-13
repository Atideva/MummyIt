using System.Collections.Generic;
using Pools;
using UnityEngine;

public class EnemyBulletStorage : MonoBehaviour
{
    public Transform damageTriggerPos;
    
    #region Singleton

    //-------------------------------------------------------------
    public static EnemyBulletStorage Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // transform.SetParent(null);
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //-------------------------------------------------------------

    #endregion

    readonly Dictionary<EnemyBullet , EnemyBulletPool> _pools = new();

    public EnemyBullet Get(EnemyBullet bullet) 
        => Pool(bullet).Get();

    EnemyBulletPool Pool(EnemyBullet bullet)
    {
        if (_pools.ContainsKey(bullet)) return _pools[bullet];

        var container = new GameObject {name = "Pool: " +bullet.name};
        container.transform.SetParent(transform);

        var pool = container.AddComponent<EnemyBulletPool>();
        pool.SetPrefab(bullet);

        _pools.Add(bullet, pool);
        return _pools[bullet];
    }
}
