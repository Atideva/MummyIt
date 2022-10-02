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
    public TextMeshProUGUI sellTxt;

    [Header("DEBUG")]
    public Item item;

    public event Action<ItemSlot> OnSell = delegate { };
    public event Action<ItemSlot> OnUse = delegate { };

    void Awake()
    {
        sellButton.gameObject.SetActive(false);
        sellButton.onClick.AddListener(Sell);
    }

    public int Price { get; private set; }

    public void SetPrice(float sellMult)
    {
        sellButton.gameObject.SetActive(true);
        Price = (int) (item.sellPrice * sellMult);
        sellTxt.text = Price.ToString();
    }


    public void Set(Item newItem)
    {
        item = newItem;
        icon.sprite = newItem.Icon;
        typeImage.color = newItem.TypeColor;
        patternUI.Set(newItem.Patterns);

#if UNITY_EDITOR
        name = "Item: " + newItem.Name;
#endif
    }

    public void Reset()
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