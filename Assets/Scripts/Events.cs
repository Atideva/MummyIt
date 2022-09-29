using System;
using UnityEngine;

public class Events : MonoBehaviour
{
    #region Singleton

    //-------------------------------------------------------------
    public static Events Instance;

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

    public event Action<Enemy> OnEnemyDeath = delegate { };
    public void EnemyDeath(Enemy enemy) => OnEnemyDeath(enemy);


    public event Action<AmmoConfig> OnAmmoAdd = delegate { };
    public void AddAmmo(AmmoConfig ammo) => OnAmmoAdd(ammo);

    public event Action<Enemy, float> OnEnemyAttack = delegate { };
    public void EnemyAttack(Enemy enemy, float damage) => OnEnemyAttack(enemy, damage);
}