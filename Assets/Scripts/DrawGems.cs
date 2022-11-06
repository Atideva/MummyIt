using System;
using System.Collections.Generic;
using AudioSystem;
using DG.Tweening;
using Pools;
using UnityEngine;

public class DrawGems : MonoBehaviour
{
    public Drawer drawer;
    public DrawMatch drawMatch;
    public DrawGemPool pool;
    public DrawGem prefab;
    public List<DrawGem> gems = new();
    public Transform collectPos;
    public float moveDur;

    
    void Awake()
    {
        pool.SetPrefab(prefab);
        drawMatch.OnMatch += Match;
        drawMatch.OnDontMatch += DontMatch;
        drawMatch.OnEmptyRelease += Release;
    }

    void DontMatch()
    {
        TurnOffGems();
        drawer.ПотушитьLines();
    }

    void Match(List<Pattern> draw)
    {
        HighlightGems();
        drawer.HighlightLines();
    }

    public void Spawn(DrawSlot drawSlot)
    {
        var gem = pool.Get();
        gem.DisableHighlight();
        gem.transform.position = drawSlot.transform.position;
        gem.transform.localScale = Vector3.one;
        gems.Add(gem);
    }

    public void Collect()
    {
        Debug.Log("Draw gems: Collect");
        foreach (var gem in gems)
        {
            gem.transform.DOScale(0.5f, moveDur);
            gem.transform.DOMove(collectPos.position, moveDur)
                .OnComplete(() => MoveFinish(gem));
        }

        gems = new List<DrawGem>();
    }

    public void Release()
    {
        foreach (var gem in gems)
            gem.ReturnToPool();

        gems.Clear();
    }

    void MoveFinish(DrawGem gem)
    {
        SimpleDataShit.Instance.AddGem();
        gem.ReturnToPool();
    }


    public void HighlightGems()
    {
        foreach (var gem in gems)
            gem.EnableHighlight();
    }

    public void TurnOffGems()
    {
        foreach (var gem in gems)
            gem.DisableHighlight();
    }

    // void FixedUpdate()
    // {
    //     if (gems.Count == 0) return;
    //
    //     if (itemHandler.AnyMatch)
    //     {
    //         HighlightGems();
    //         drawer.HighlightLines();
    //     }
    //     else
    //     {
    //         TurnOffGems();
    //         drawer.ПотушитьLines();
    //     }
    // }
}