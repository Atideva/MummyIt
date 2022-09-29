using System;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    public int killsForLevel;
    public int killsInc;
    [Header("DEBUG")]
    public int currentLevel;
    public int currentXp;
    public int requireXp;
    public event Action<int> OnLevelUp = delegate { };

    void Start()
    {
        Events.Instance.OnEnemyDeath += OnEnemyDeath;
        RequireXp();
    }

    void OnEnemyDeath(Enemy enemy)
    {
        AddExp(1);
    }

    void RequireXp()
    {
        requireXp = killsForLevel + killsInc * currentLevel;
    }

    public void AddExp(int amount)
    {
        currentXp += amount;

        if (currentXp >= requireXp)
        {
            currentLevel++;
            OnLevelUp(currentLevel);
            RequireXp();
            currentXp = 0;
        }
    }
}