using System;
using UnityEngine;
using UnityEngine.UI;

public class GunSlot : MonoBehaviour
{
    public GunConfig gun;
    public Image icon;
    public Button clickButton;
    public event Action<GunSlot> OnClick = delegate { };

    void Awake()
    {
        clickButton.onClick.AddListener(Click);
    }

    void Click() => OnClick(this);

    public void SetGun(GunConfig newGun)
    {
        gun = newGun;
        icon.sprite = newGun.Icon;
    }
}