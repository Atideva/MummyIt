using AttackModificators;
using UnityEngine;

public class Teleport : EnemyAbility
{
    [Header("Setup")]
    public float teleportFromY;
    public float teleportToY;
    public float teleportDuration;
    [Header("VFX")]
    public VFX startVFX;
    public VFX endVFX;
    bool _jumpDone;
    bool _isJumping;
    Vector3 _teleportPos;
    bool _dead;

    protected override void OnInit()
    {
        Owner.hp.OnDeath += OnDeath;
    }

    void OnDeath()
    {
        _dead = true;
    }

    void FixedUpdate()
    {
        if (_dead) return;
        if (_jumpDone) return;

        if (!_isJumping && Owner.transform.position.y < teleportFromY)
        {
            _isJumping = true;
            Hide();
        }

        if (_isJumping)
        {
            Invoke(nameof(Show), teleportDuration);
            _jumpDone = true;
        }
    }

    void Hide()
    {
        if (_dead) return;
        var pos = Owner.transform.position;
        _teleportPos = new Vector3(pos.x, teleportToY, pos.z);
        Events.Instance.PlayVfx(startVFX, pos);
        Owner.gameObject.SetActive(false);
    }

    void Show()
    {
        if (_dead) return;
        Owner.transform.position = _teleportPos;
        Events.Instance.PlayVfx(endVFX, _teleportPos);
        Owner.gameObject.SetActive(true);
    }


    public override void Reset()
    {
        _jumpDone = false;
        _isJumping = false;
        _dead = false;
    }
}