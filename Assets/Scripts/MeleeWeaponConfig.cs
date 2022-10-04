using System.Collections;
using System.Collections.Generic;
using AudioSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "Melee Weapon", menuName = "Configs/New Melee Weapon")]
public class MeleeWeaponConfig : ScriptableObject
{
    [SerializeField] Sprite sprite;
    [SerializeField]  AudioData hitSound;
    [SerializeField]  float damage;
    [SerializeField]  float cooldown;

    public float Cooldown => cooldown;
    public float Damage => damage;
    public AudioData HitSound => hitSound;
    public Sprite WeaponSprite => sprite;
    
}
