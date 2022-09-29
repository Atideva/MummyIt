using System.Collections.Generic;
using UnityEngine;

public class ItemTransporter : MonoBehaviour
{
    public RectTransform container;
    public ItemSpawner spawner;

    public float moveSpeed;
    [Header("DEBUG")]
    public float leftCorner;
    public float rightCorner;
    public float itemWidth;
    IReadOnlyList<ItemSlot> Items => spawner.lineItems;


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
        for (var i = 0; i < Items.Count; i++)
        {
            var moveTo = i == 0 ? leftCorner : GetRightBorder(Items[i - 1]);

            var x = Items[i].slotRect.anchoredPosition.x;
            var y = Items[i].slotRect.anchoredPosition.y;

            if (x > moveTo)
            {
                x -= moveSpeed * Time.deltaTime;
                if (x < moveTo) x = moveTo;
                Items[i].slotRect.anchoredPosition = new Vector2(x, y);
            }
        }
    }
}