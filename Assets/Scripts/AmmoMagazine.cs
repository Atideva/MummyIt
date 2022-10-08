using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class AmmoMagazine : MonoBehaviour
{
    [SerializeField] List<AmmoConfig> ammoTypes = new();
    [SerializeField] AnimationCurve ammoCurve;
    [SerializeField] float chanceFactor = 4f;
    public TextMeshProUGUI ammoText;
    [Header("DEUBG")]
    [SerializeField] int ammo;
    public IReadOnlyList<AmmoConfig> AmmoTypes => ammoTypes;

    void Awake()
    {
        ammo = 0;
        RefreshText();
    }

    public void TakeAmmo()
    {
        ammo--;
        RefreshText();
    }

    public void AddAmmo(int amount)
    {
        ammo += amount;
        RefreshText();
    }

    void RefreshText()
        => ammoText.text = ammo.ToString();


    public float GetAmmoChance(int ammoID)
    {
        var point = ammoTypes.Count > 1 ? (float) ammoID / (ammoTypes.Count - 1) : 0;
        var value = ammoCurve.Evaluate(point);

        var factor = 1 / chanceFactor;
        var factorValue = value + factor;
        var factorTotal = TotalChance + factor * ammoTypes.Count;

        return factorTotal > 0 ? factorValue / factorTotal : value;
    }

    float TotalChance => ammoTypes
        .Select((t, i) => i / (float) (ammoTypes.Count - 1))
        .Sum(ammoCurve.Evaluate);

    public int Ammo => ammo;
}