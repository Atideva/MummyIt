using System;
using UnityEngine;
using UnityEngine.UI;

public class GunSlot : MonoBehaviour
{
    public GunConfig gun;
    public Image icon;
    public Button clickButton;
    public Image rank;
    public Image atkModificator;
    public void Enable() => gameObject.SetActive(true);
    public void Disable() => gameObject.SetActive(false);
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

    public void SetModificator(Sprite sprite)
    {
        atkModificator.enabled = true;
        atkModificator.sprite = sprite;
    }

    public void DisableModificator() => atkModificator.enabled = false;
    public void SetRank(Sprite sprite)
    {
        rank.enabled = true;
        rank.sprite = sprite;
    }

    
    public void DisableRank() => rank.enabled = false;
}