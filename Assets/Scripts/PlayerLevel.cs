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
        Events.Instance.OnLevelUp += OnExtraLevelUp;
        CalcNextXp();
    }

    void OnExtraLevelUp()
        => LevelUp();

    void OnEnemyDeath(Enemy enemy)
        => AddExp(1);

    void CalcNextXp()
        => requireXp = killsForLevel + killsInc * currentLevel;

    void AddExp(int amount)
    {
        currentXp += amount;
        if (currentXp >= requireXp)
            LevelUp();
    }

    void LevelUp()
    {
        currentLevel++;
        OnLevelUp(currentLevel);
        CalcNextXp();
        currentXp = 0;
    }
}