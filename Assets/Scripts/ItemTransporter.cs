using System.Collections.Generic;
using UnityEngine;

public class ItemTransporter : MonoBehaviour
{
    public RectTransform container;
    public ItemSpawner spawner;

    public float moveSpeed;
    public float leftCorner;
    public bool deleteItems;
    public float deleteItemCorner;
    [Header("DEBUG")]
    public float rightCorner;
    public float itemWidth;
    IReadOnlyList<ItemSlot> Items => spawner.LineItems;

    public bool IsVisible(ItemSlot slot)
    {
        var x = slot.slotRect.anchoredPosition.x;
        return x <= rightCorner;
    }

    // rect.anchoredPosition = new Vector3(i * stepWidth, 0);
    void Start()
    {
        rightCorner = container.rect.width;
        itemWidth = spawner.slotPrefab.slotRect.rect.width;

        spawner.OnCreate += OnCreate;
    }

    float GetRightBorder(ItemSlot slot) => slot.slotRect.anchoredPosition.x + itemWidth;

    void OnCreate(ItemSlot item)
    {
        item.transform.SetParent(container);
        item.transform.localScale = Vector3.one;

        var lastItemCorner = Items.Count > 1 ? GetRightBorder(Items[^2]) : leftCorner;

        var x = rightCorner > lastItemCorner ? rightCorner : lastItemCorner;
        var y = 0;

        item.slotRect.sizeDelta = new Vector2(item.slotRect.rect.width, container.rect.height);
        item.slotRect.anchoredPosition = new Vector2(x, y);
    }

    void Update()
    {
        var toRemove = new List<ItemSlot>();
        for (var i = 0; i < Items.Count; i++)
        {
            var moveTo = i == 0 ? leftCorner : GetRightBorder(Items[i - 1]);

            var x = Items[i].slotRect.anchoredPosition.x;
            var y = Items[i].slotRect.anchoredPosition.y;

            if (deleteItems && x <= deleteItemCorner)
            {
                toRemove.Add(Items[i]);
            }

            if (x > moveTo)
            {
                x -= moveSpeed * Time.deltaTime;
                if (x < moveTo) x = moveTo;
                Items[i].slotRect.anchoredPosition = new Vector2(x, y);
            }
        }

        foreach (var r in toRemove)
        {
            r.ReturnToPool();
        }
    }
}