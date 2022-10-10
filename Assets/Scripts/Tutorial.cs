using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public List<TutorialStep> steps = new();
    public int current;
    public EnemySpawner enemySpawner;
    public ItemGetter itemGetter;
    public ItemSpawner itemSpawner;
    
    void Start()
    {
        for (var i = 0; i < steps.Count; i++)
        {
            steps[i].id = i;
            steps[i].OnComplete += OnComplete;
            steps[i].Init(this);
        }

        Invoke(nameof(DisableEnemySpawn), 0.1f);

        current = 0;
        StartCurrentStep();
    }

    void DisableEnemySpawn() => enemySpawner.StopSpawn();

    void OnComplete(TutorialStep step)
    {
        current++;
        if (current < steps.Count)
        {
            StartCurrentStep();
        }
    }

    void StartCurrentStep() => steps[current].StartStep();
}