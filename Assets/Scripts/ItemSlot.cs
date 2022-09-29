using System;
using Items;
using Pools;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : PoolObject
{
    [Header("Setup")]
    public RectTransform slotRect;
    public Image icon;
    public Image typeImage;
    public RectTransform container;
    public Button button;
    public PatternUI patternUI;

    [Header("DEBUG")]
    public Item item;

    public event Action<ItemSlot> OnClick = delegate { };
    public event Action<ItemSlot> OnUse = delegate { };

    void Awake() => button.onClick.AddListener(Click);
    void Click() => OnClick(this);

    public void Set(Item newItem)
    {
        item = newItem;
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
}