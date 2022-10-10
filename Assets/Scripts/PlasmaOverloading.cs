using System;
using EPOOutline;
using Powerups;
using UI;
using UnityEngine;

public class PlasmaOverloading : MonoBehaviour
{
    public CustomSlider slider;
    bool _plasmaOverload;
    float _plasmaOverloadTimer;
    float _plasmaAtkSpdBonus;
    public Outlinable firstGunOutline;
    public Outlinable secondGunOutline;
    public bool Active => _plasmaOverload;
    public bool Disabled => !_plasmaOverload;
    public float AtkSpeed => _plasmaAtkSpdBonus;
    public event Action<PlasmaOverloadData> OnOverloadStart = delegate { };
    public event Action OnOverloadEnd = delegate { };
    bool secondGunEnabled;
    public void SecondGunEnabled() => secondGunEnabled = true;
    float _lastMaxCd;

    void Awake()
    {
        slider.gameObject.SetActive(false);
        firstGunOutline.enabled = false;
        secondGunOutline.enabled = false;
    }

    void Start()
    {
        Events.Instance.OnUsePlasmaOverload += OnPlasmaOverload;
    }

    void OnPlasmaOverload(PlasmaOverloadData data)
    {
        _plasmaOverload = true;
        _plasmaOverloadTimer += data.Duration;
        _lastMaxCd += data.Duration;
        _plasmaAtkSpdBonus = data.AtkSpdBonus;
        firstGunOutline.enabled = true;
        if (secondGunEnabled) secondGunOutline.enabled = true;
        OnOverloadStart(data);
        slider.gameObject.SetActive(true);
    }

    void StopPlasmaOverload()
    {
        _plasmaOverload = false;
        _plasmaOverloadTimer = 0;
        _lastMaxCd = 0;
        _plasmaAtkSpdBonus = 0;
        firstGunOutline.enabled = false;
        if (secondGunEnabled) secondGunOutline.enabled = false;
        OnOverloadEnd();
        slider.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        if (Disabled) return;

        if (_plasmaOverloadTimer > 0)
        {
            _plasmaOverloadTimer -= Time.fixedDeltaTime;
            slider.SetValue(_plasmaOverloadTimer / _lastMaxCd);
        }
        else
            StopPlasmaOverload();
    }
}