using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoMagazine : MonoBehaviour
{
    [SerializeField] List<AmmoConfig> ammoTypes = new();
    [SerializeField] AnimationCurve ammoCurve;
    [SerializeField] float chanceFactor = 4f;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] Image ammoIcon;
    // ReSharper disable once StringLiteralTypo
    [Header("DEBUBG")]
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
        ammoText.transform.DOScale(1.3f, 0.15f)
            .OnComplete(()
                => ammoText.transform.DOScale(1, 0.15f));
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