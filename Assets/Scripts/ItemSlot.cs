using System;
using Items;
using Pools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : PoolObject
{
    [Header("Setup")]
    public RectTransform slotRect;
    public Image icon;
    public Image typeImage;
    public RectTransform container;
    public PatternUI patternUI;
    public Button sellButton;
    public Button clickButton;
    public TextMeshProUGUI sellTxt;

    [Header("DEBUG")]
    public Item item;
    public int Price { get; private set; }
    public event Action<ItemSlot> OnSell = delegate { };
    public event Action<ItemSlot> OnClick = delegate { };
    public event Action<ItemSlot> OnUse = delegate { };

    void Awake()
    {
        sellButton.gameObject.SetActive(false);
        sellButton.onClick.AddListener(Sell);
        clickButton.onClick.AddListener(Click);
    }

    void Click()
        => OnClick(this);
    


    public void SetPrice(float sellMult)
    {
        sellButton.gameObject.SetActive(true);
        Price = (int) (item.sellPrice * sellMult);
        sellTxt.text = Price.ToString();
    }


    public void Set(Item newItem)
    {
        ResetContainerPosition();
        item = newItem;
        icon.sprite = newItem.Icon;
        typeImage.color = newItem.TypeColor;
        patternUI.Set(newItem.Patterns);

#if UNITY_EDITOR
        name = "Item: " + newItem.Name;
#endif
    }

      void ResetContainerPosition()
        => container.anchoredPosition = Vector2.zero;

    public void Use()
    {
        item.Use();
        OnUse(this);
        patternUI.Disable();
        ReturnToPool();
    }

    void Sell()
    {
        OnSell(this);
        patternUI.Disable();
        ReturnToPool();
    }
}