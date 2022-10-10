using System.Collections.Generic;
using System.Linq;
using Ranged;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Configs/New Gun")]
public class GunConfig : ScriptableObject
{
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] Sprite icon;
    [SerializeField] Sprite sprite;
    [SerializeField] float damage = 10;
    [SerializeField] float bulletSpeed = 10;
    [SerializeField] float fireRate = 3;
    [SerializeField] bool multiShot;
    [SerializeField] int bulletsAmountPerShot;
    [SerializeField] float delayBetweenShots;
    //   [SerializeField] float angleSpread;
    [SerializeField]     [RangeInt(0, 180)] RangedInt angleSpread;
    [SerializeField][TextArea] string description;
    [SerializeField] int maxUpgradeLevel;
  //  [RangeFloat(0, 3)] public RangedFloat pitch;
    //[SerializeField] int magazine = 6;
    //[SerializeField] float reloadTime = 0.5f;
    //
    // [Header("AMMO")]
    // [SerializeField] List<AmmoConfig> ammo = new();
    // [SerializeField] AnimationCurve ammoCurve;
    // [SerializeField] float chanceFactor = 4f;
    // public IReadOnlyList<AmmoConfig> Ammo => ammo;
    public Sprite Icon => icon;
    public float FireRate => fireRate;
    public Sprite Sprite => sprite;
    public string Description => description;
    // public float GetAmmoChance(int ammoID)
    // {
    //     var point = ammo.Count > 1 ? (float) ammoID / (ammo.Count - 1) : 0;
    //     var value = ammoCurve.Evaluate(point);
    //
    //     var factor = 1 / chanceFactor;
    //     var factorValue = value + factor;
    //     var factorTotal = TotalChance + factor * ammo.Count;
    //
    //     return factorTotal > 0 ? factorValue / factorTotal : value;
    // }
    //
    // float TotalChance => ammo
    //     .Select((t, i) => i / (float) (ammo.Count - 1))
    //     .Sum(ammoCurve.Evaluate);

    public Bullet BulletPrefab => bulletPrefab;

   public RangedInt AngleSpread => angleSpread;

    public int BulletsAmountPerShot => bulletsAmountPerShot;

    public bool MultiShot
    {
        get => multiShot;
        set => multiShot = value;
    }

    public float DelayBetweenShots => delayBetweenShots;

    public int MaxUpgradeLevel => maxUpgradeLevel;

    public float Damage => damage;

    public float BulletSpeed => bulletSpeed;
}