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
    public float MoveToStartCorner => 0 - itemWidth;
    float GetItemRightX(ItemSlot slot) => slot.slotRect.anchoredPosition.x + itemWidth;

    public event Action<ItemSlot> OnMoveOutOfScreen = delegate { };
    public event Action<ItemSlot> OnBecomeVisible = delegate { };
 
    IReadOnlyList<ItemSlot> Slots => slotSpawner.Slots;


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

            var x = itemWidth * i + paddingX * i;
            var y = 0;
            //slot.slotRect.sizeDelta = new Vector2(slot.slotRect.rect.width, container.rect.height);
            slot.slotRect.anchoredPosition = new Vector2(x, y);
        }
    }

    public bool OutScreenRight(ItemSlot slot)
    {
        var x = slot.slotRect.anchoredPosition.x;
        return x > rightCorner;
    }

    public float visibilityThreshold = 100;

    public bool VisibleAtScreen(ItemSlot slot)
    {
        var x = slot.slotRect.anchoredPosition.x;
        return VisibleAtScreen(x);
    }

    bool VisibleAtScreen(float xPos)
        => xPos <= rightCorner - visibilityThreshold;

    bool NotVisibleAtScreen(float xPos)
        => !VisibleAtScreen(xPos);

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
        MoveToStart(slot);
        //slot.slotRect.sizeDelta = new Vector2(slot.slotRect.rect.width, container.rect.height);
    }

    public float paddingX = 10;

    void MoveToStart(ItemSlot slot)
    {
        var lastItemCorner = Slots.Count > 1 ? GetItemRightX(Slots[^1]) : leftCorner;
        var x = lastItemCorner + paddingX;
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

        ItemSlot moveSlot = null;
        foreach (var slot in Slots)
        {
            //  var moveTo = i == 0 ? leftCorner : GetItemRightX(Items[i - 1]);
            var pos = slot.slotRect.anchoredPosition;
            var x = pos.x;
            var y = pos.y;
            var move = moveSpeed * Time.deltaTime;

            if (BecomeVisible(x, move))
            {
                OnBecomeVisible(slot);
            }

            x -= move;
            pos = new Vector2(x, y);
            slot.slotRect.anchoredPosition = pos;

            if (x <= MoveToStartCorner)
            {
                moveSlot = slot;

            }
        }

        if (moveSlot)
        {
            MoveToStart(moveSlot);
            OnMoveOutOfScreen(moveSlot);
        }

 
    }

    bool BecomeVisible(float x, float move)
    {
        return NotVisibleAtScreen(x) && VisibleAtScreen(x - move);
    }
}