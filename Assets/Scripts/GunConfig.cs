using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "Gun", menuName = "Configs/New Gun")]
public class GunConfig : ScriptableObject
{
    [SerializeField] Sprite icon;
    [SerializeField] Sprite sprite;
    [SerializeField] float fireRate = 3;
 //   [SerializeField] int magazine = 6;
 //   [SerializeField] float reloadTime = 0.5f;
    [Header("AMMO")]
    [SerializeField] List<AmmoConfig> ammo = new();
    [SerializeField] AnimationCurve ammoCurve;
    [SerializeField]   float chanceFactor = 4f;
    public IReadOnlyList<AmmoConfig> Ammo => ammo;

    public Sprite Icon => icon;

    public float FireRate => fireRate;

    public Sprite Sprite => sprite;

    public float GetAmmoChance(int ammoID)
    {
        var point = ammo.Count > 1 ? (float) ammoID / (ammo.Count - 1) : 0;
        var value = ammoCurve.Evaluate(point);
        
        var factor = 1 / chanceFactor;
        var factorValue = value + factor;
        var factorTotal = TotalChance + factor * ammo.Count;
        
        return factorTotal > 0 ? factorValue / factorTotal : value;
    }

    float TotalChance => ammo
        .Select((t, i) => i / (float) (ammo.Count - 1))
        .Sum(ammoCurve.Evaluate);
}