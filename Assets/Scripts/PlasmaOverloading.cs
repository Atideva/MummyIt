using System;
using EPOOutline;
using Powerups;
using UI;
using UnityEngine;

public class PlasmaOverloading : MonoBehaviour
{
    public Sprite bulletSprite;
    public float bulletSize=1.5f;
    public CustomSlider slider;
    bool _overload;
    float _plasmaOverloadTimer;
    float _atkSpdBonus;
    public Gun firstGun;
    public Gun secondGun;
    public bool Active => _overload;
    public bool Disabled => !_overload;
    public float AtkSpeed => _atkSpdBonus;
    public event Action<PlasmaOverloadData> OnOverloadStart = delegate { };
    public event Action OnOverloadEnd = delegate { };
    bool _isSecondGun;
    public void SecondGunEnabled() => _isSecondGun = true;
    float _lastMaxCd;

    void Awake()
    {
        slider.gameObject.SetActive(false);
        if (firstGun.CurrentView) firstGun.CurrentView.DisableOutline();
        if (secondGun.CurrentView) secondGun.CurrentView.DisableOutline();
    }

    void Start()
    {
        Events.Instance.OnUsePlasmaOverload += OnPlasmaOverload;
    }

    void OnPlasmaOverload(PlasmaOverloadData data)
    {
        _overload = true;
        _plasmaOverloadTimer += data.Duration;
        _lastMaxCd += data.Duration;
        _atkSpdBonus = data.AtkSpdBonus;
        if (firstGun.CurrentView) firstGun.CurrentView.EnableOutline();
        if (secondGun.CurrentView) secondGun.CurrentView.EnableOutline();
        OnOverloadStart(data);
        slider.gameObject.SetActive(true);
    }

    void StopPlasmaOverload()
    {
        _overload = false;
        _plasmaOverloadTimer = 0;
        _lastMaxCd = 0;
        _atkSpdBonus = 0;
        if (firstGun.CurrentView) firstGun.CurrentView.DisableOutline();
        if (secondGun.CurrentView) secondGun.CurrentView.DisableOutline();
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