using System;
using UnityEngine;
using UnityEngine.UI;

public class GunSlot : MonoBehaviour
{
    public GunConfig gun;
    public Image icon;
    public Button clickButton;
    public Image rank;
    public GameObject rankCont;
    public Image atkModificator;
    public GameObject atkModificatorCont;
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
        atkModificatorCont.SetActive(true);
        atkModificator.sprite = sprite;
    }

    public void DisableModificator()
        => atkModificatorCont.SetActive(false);

    public void SetRank(Sprite sprite)
    {
        rankCont.SetActive(true);

        rank.sprite = sprite;
    }


    public void DisableRank() 
        => rankCont.SetActive(false);
    
}