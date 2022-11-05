using System;
using System.Collections.Generic;
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
    public Image typeImage2;
    public RectTransform container;
    public PatternUI patternUI;
    public Button sellButton;
    public Button clickButton;
    public TextMeshProUGUI sellTxt;
    public TextMeshProUGUI ammoCountTxt;
    public Image highlightImage;
    [Header("DEBUG")]
    public Item item;
    public int Price { get; private set; }
    public event Action<ItemSlot> OnSell = delegate { };
    public event Action<ItemSlot> OnClick = delegate { };
    public event Action<ItemSlot> OnUse = delegate { };
    public event Action<ItemSlot> OnDestroy = delegate { };
    public bool HasItem => item;
    public bool IsEmpty => !item;
    public bool Enabled => gameObject.activeSelf;
    public int PatternsCount => IsEmpty ? 0 : item.Patterns.Count;
    public  IReadOnlyList<Pattern> Patterns => IsEmpty ? null : item.Patterns;
    public void EnableHighlight()
    {
        highlightImage.enabled = true;
    }
    public void DisableHighlight()
    {
        highlightImage.enabled = false;
    }
    public void Empty()
    {
        item = null;
        container.gameObject.SetActive(false);
        patternUI.Disable();
    }

    void Awake()
    {
        sellButton.gameObject.SetActive(false);
        sellButton.onClick.AddListener(Sell);
        clickButton.onClick.AddListener(Click);
        OnReturnToPool += OnReturn;
    }

    void OnReturn()
    {
        patternUI.Disable();
        OnDestroy(this);
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
        ResetContainer();
        item = newItem;
        icon.sprite = newItem.Icon;
        patternUI.Set(newItem.Patterns);
        typeImage.sprite = newItem.TypeSprite;
        typeImage2.sprite = newItem.TypeSprite2;
        if (newItem is ItemAmmo ammo)
        {
            ammoCountTxt.enabled = true;
            ammoCountTxt.text = ammo.Ammo.Amount.ToString();
        }
        else
        {
            ammoCountTxt.enabled = false;
        }
        
#if UNITY_EDITOR
        name = "Item: " + newItem.Name;
#endif
    }

    void ResetContainer()
    {
        container.gameObject.SetActive(true);
        container.anchoredPosition = Vector2.zero;
        container.localScale=Vector3.one;
    }

    public void Use()
    {
        item.Use();
        OnUse(this);
 
    }

    void Sell()
    {
        OnSell(this);
        ReturnToPool();
    }
}