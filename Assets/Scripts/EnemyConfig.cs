using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Configs/New Enemy")]
public class EnemyConfig : ScriptableObject
{
    public Enemy prefab;
    public float damage = 10f;
    public float attackSpeed = 1;
    public float speed = 0.5f;
    public Sprite patternIcon;
    public Sprite enemyIcon;
    public List<Pattern> patterns = new();
}