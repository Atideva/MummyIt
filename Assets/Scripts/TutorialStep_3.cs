using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStep_3 : TutorialStep
{
    public List<TutEnemySpawnData> spawnEnemies = new();
    public List<TutPowerUpSpawnData> spawnsItems = new();
    public HandPointer pointerPrefab;
    HandPointer _pointer;
    public Transform pointerContainer;

    protected override void OnStart()
    {
        foreach (var data in spawnEnemies)
        {
            Events.Instance.SpawnEnemy(data.enemy, data.spawnPos.position);
        }

        foreach (var data in spawnsItems)
        {
            var item = Tutorial.itemGetter.GetPowerupItem(data.item);
            Events.Instance.SpawnItem(item, data.posX);
        }

        _pointer = Instantiate(pointerPrefab, pointerContainer);
        Tutorial.itemSpawner.OnSlotClick += OnSlotClick;
        Events.Instance.OnEnemyDeath += OnEnemyDeath;
    }

    void OnSlotClick(ItemSlot slot)
    {
        _pointer.Disable();
        Tutorial.itemSpawner.OnSlotClick -= OnSlotClick;
    }

    int _complete;

    void OnEnemyDeath(Enemy enemy)
    {
        _complete++;
        if (_complete == spawnEnemies.Count)
            Complete();
    }
}
