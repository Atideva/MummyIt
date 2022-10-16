using AttackModificators;
using AudioSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Configs/New Enemy")]
public class EnemyConfig : ScriptableObject
{
    [Header("Settings")]
    public float hitpoints = 10f;
    public float moveSpeed = 0.5f;
    public float damage = 10f;
    public float attackSpeed = 1;
    [Header("Setup")]
    public Enemy prefab;
    public Sprite enemyIcon;
    [Header("VFX")]
    public float attackVfxDelay;
    public VFX attackVfx;
    public VFX deathVfx;
    [Header("Sounds")]
    public AudioData attackSound;
    public AudioData deathSound;

    // public Sprite patternIcon;
    //  public List<Pattern> patterns = new();
}