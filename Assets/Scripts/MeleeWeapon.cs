using System;
using DG.Tweening;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public MeleeWeaponConfig weapon;
    public float getTargetsFreq = 0.5f;
    public DOTweenAnimation attackAnim;
    public MeleeWeaponTrigger trigger;
    public MeleeWeaponCollider wepCollider;
    public SpriteRenderer weaponSprite;
    [Header("DEBUG")]
    public float shootTimer;
    public float targetsTimer;
    bool _isTargetNear;
    public bool IsEnable { get; private set; }

    void Awake()
    {
        Disable();
    }

    public void Disable()
    {
        weaponSprite.enabled = false;
    }

    public void Enable()
    {
        if (IsEnable) return;
        IsEnable = true;
        trigger.Init(this);
        targetsTimer = 0;
        shootTimer = 0;
        weaponSprite.enabled = true;
    }

    public void Refresh(MeleeWeaponConfig wep)
        => weapon = wep;

    public void NoTargets()
        => _isTargetNear = false;

    public void TargetsAcquire()
        => _isTargetNear = true;

    void FixedUpdate()
    {
        if (!IsEnable) return;

        shootTimer -= Time.fixedDeltaTime;
        targetsTimer -= Time.fixedDeltaTime;

        if (shootTimer > 0) return;
        if (targetsTimer > 0) return;

        CheckTargetsNear();

        if (!_isTargetNear) return;
        Attack();
    }

    void CheckTargetsNear()
    {
        if (trigger.Searching) return;
        _isTargetNear = false;
        trigger.SearchForTargets();
        targetsTimer = getTargetsFreq;
    }

    void Attack()
    {
        shootTimer = weapon.Cooldown;
        attackAnim.DORestart();
        attackAnim.DOPlay();
        wepCollider.Enable(weapon, attackAnim.duration);
    }
}