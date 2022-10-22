using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemTransporter : MonoBehaviour
{
    [Header("Setup")]
    public RectTransform container;
    public ItemSlotSpawner slotSpawner;
    public ItemSpawner itemSpawner;

    [Header("Settings")]
    public float moveSpeed;
    public float leftCorner;

    [Header("DEBUG")]
    public float rightCorner;
    public float itemWidth;
    float _delayBeforeStart;

    IReadOnlyList<ItemSlot> Slots => slotSpawner.Slots;
    public event Action<ItemSlot> OnMoveToLineEnd = delegate { };
    public float SwapCorner => 0 - itemWidth;
    float GetItemRightX(ItemSlot slot) => slot.slotRect.anchoredPosition.x + itemWidth;

    public void SetBaseSlotsPos(List<ItemSlot> slots)
    {
        if (slots.Count == 0)
        {
            Debug.LogError("WTF, why is there 0 slots, uh?");
            return;
        }

        itemWidth = slotSpawner.slotPrefab.slotRect.rect.width;
        for (var i = 0; i < slots.Count; i++)
        {
            var slot = slots[i];
            slot.transform.SetParent(container);
            slot.transform.localScale = Vector3.one;

            var x = itemWidth * i+paddingX*i;
            var y = 0;
            //slot.slotRect.sizeDelta = new Vector2(slot.slotRect.rect.width, container.rect.height);
            slot.slotRect.anchoredPosition = new Vector2(x, y);
        }
    }

    public bool OutRightSideScreen(ItemSlot slot)
    {
        var x = slot.slotRect.anchoredPosition.x;
        return x > rightCorner;
    }

    public bool VisibleAtScreen(ItemSlot slot)
    {
        var x = slot.slotRect.anchoredPosition.x;
        return x <= rightCorner;
    }

    // rect.anchoredPosition = new Vector3(i * stepWidth, 0);
    void Awake()
    {
        slotSpawner.OnCreate += OnCreate;
        _delayBeforeStart = itemSpawner.Cooldown;
    }

    void Start()
    {
        rightCorner = container.rect.width;
        itemWidth = slotSpawner.slotPrefab.slotRect.rect.width;
    }


    void OnCreate(ItemSlot slot)
    {
        slot.transform.SetParent(container);
        slot.transform.localScale = Vector3.one;
        MoveToLineEnd(slot);
        //slot.slotRect.sizeDelta = new Vector2(slot.slotRect.rect.width, container.rect.height);
    }

    public float paddingX=10;
    void MoveToLineEnd(ItemSlot slot)
    {
        var lastItemCorner = Slots.Count > 1 ? GetItemRightX(Slots[^1]) : leftCorner;
        var x = lastItemCorner+paddingX;
        var y = 0;
        slot.slotRect.anchoredPosition = new Vector2(x, y);
    }

    void Update()
    {
        if (_delayBeforeStart > 0)
        {
            _delayBeforeStart -= Time.deltaTime;
            return;
        }

        ItemSlot swapSlot = null;
        foreach (var slot in Slots)
        {
            //  var moveTo = i == 0 ? leftCorner : GetItemRightX(Items[i - 1]);
            var pos = slot.slotRect.anchoredPosition;
            var x = pos.x;
            var y = pos.y;
            //if (x > moveTo){
            x -= moveSpeed * Time.deltaTime;
            //if (x < moveTo) x = moveTo;
            pos = new Vector2(x, y);
            slot.slotRect.anchoredPosition = pos;
            //}
            if (x <= SwapCorner)
                swapSlot = slot;
        }

        if (swapSlot)
        {
            MoveToLineEnd(swapSlot);
            OnMoveToLineEnd(swapSlot);
        }
    }
}