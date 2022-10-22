using System;
using System.Collections.Generic;
using DG.Tweening;
using Pools;
using UnityEngine;

public class DrawGemsSpawner : MonoBehaviour
{
    
    public PatternDrawer drawer;
    public ItemHandler itemHandler;
    public DrawGemPool pool;
    public DrawGem prefab;
    public List<DrawGem> gemInSlots = new();
    public Transform collectPos;
    public float moveDur;

    void Awake()
    {
        pool.SetPrefab(prefab);
    }

    public void Spawn(DrawSlot drawSlot)
    {
        var gem = pool.Get();
        gem.DisableHighlight();
        gem.transform.position = drawSlot.transform.position;
        gem.transform.localScale = Vector3.one;
        gemInSlots.Add(gem);
    }

    public void Collect()
    {
        Debug.Log("Draw gems: Collect");
        foreach (var gem in gemInSlots)
        {
            gem.transform.DOScale(0.5f, moveDur);
            gem.transform.DOMove(collectPos.position, moveDur)
                .OnComplete(() => MoveFinish(gem));
        }

        gemInSlots = new List<DrawGem>();
    }

    public void Release()
    {
        Debug.Log("Draw gems: Release");
        foreach (var gem in gemInSlots)
        {
            gem.ReturnToPool();
        }

        gemInSlots = new List<DrawGem>();
    }

    void MoveFinish(DrawGem gem)
    {
        SimpleDataShit.Instance.AddGem();
        gem.ReturnToPool();
    }


    public void HighlightGems()
    {
        foreach (var gem in gemInSlots)
            gem.EnableHighlight();
    }

    public void DisableGemsHighlights()
    {
        foreach (var gem in gemInSlots)
            gem.DisableHighlight();
    }

    void FixedUpdate()
    {
        if (gemInSlots.Count == 0) return;

        if (itemHandler.AnyMatch())
        {
            HighlightGems();
            drawer.HighlightLines();
        }
        else
        {
            DisableGemsHighlights();
            drawer.CommonLines();
        }
        
        
    }
}