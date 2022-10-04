using UnityEngine;

public class ItemMagnet : MonoBehaviour
{
    public ItemHandler itemHandler;
    [Header("DEBUG")]
    public float cooldown;
    public float timer;
    bool _isEnable;

    void Start()
    {
        Events.Instance.OnAmmoMagnet += OnAmmoMagnet;
    }

    void OnAmmoMagnet(float cd)
    {
        _isEnable = true;
        cooldown = cd;
    }

    void FixedUpdate()
    {
        if (!_isEnable) return;
        timer -= Time.fixedDeltaTime;
        if (timer > 0) return;
        if (!itemHandler.AnyAmmo) return;
        timer = cooldown;
        itemHandler.CollectAmmo();
    }
}