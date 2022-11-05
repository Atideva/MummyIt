using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Items;
using UnityEngine;

public class DrawMatch : MonoBehaviour
{
    public DrawGems drawGem;
    public ItemHandler itemHandler;
    public Drawer drawer;
    public ItemSlotSpawner spawner;
    public ItemTransporter transporter;
    public event Action<List<ItemSlot>> OnMatchRelease = delegate { };
    public event Action OnEmptyRelease = delegate { };
    public event Action<List<Pattern>> OnMatch = delegate { };
    public event Action OnDontMatch = delegate { };
    List<ItemSlot> _matched = new();

    void Start()
    {
        drawer.OnNewDraw += NewDraw;
        drawer.OnRelease += StopDraw;
        drawer.OnPatternChange += PatternChange;
        transporter.OnBecomeVisible += OnBecomeVisible;
        transporter.OnMoveOutOfScreen += OnMoveOutOfScreen;
    }

    void OnMoveOutOfScreen(ItemSlot item)
    {
        if (!_matched.Contains(item)) return;

        item.DisableHighlight();
        _matched.Remove(item);
    }

    bool LimitReach => _matched.Count >= itemHandler.pickupAtOnce;
    void OnBecomeVisible(ItemSlot item)
    {
        if (LimitReach) return;
        if (NotPickable(item)) return;
        if (NotMatch(item, drawer.drawPatterns)) return;
        
        item.EnableHighlight();
        _matched.Add(item);
        OnMatch(drawer.drawPatterns);
    }

    void PatternChange(List<Pattern> draw)
    {
        if (_matched.Count > 0)
        {
            foreach (var item in _matched)
                item.DisableHighlight();

            _matched.Clear();
        }

        _matched = FindMatch(spawner.VisibleItems, draw);
        if (_matched.Count == 0)
        {
            OnDontMatch();
            return;
        }

        foreach (var item in _matched)
            item.EnableHighlight();

        OnMatch(draw);
    }

    void NewDraw()
    {
    }

    void StopDraw(List<Pattern> patterns)
    {
        if (_matched.Count == 0)
        {
            OnEmptyRelease();
            return;
        }

        foreach (var match in _matched)
        {
            //match.DisableHighlight();
            OnMatchRelease(_matched);
        }

        _matched.Clear();
    }


    List<ItemSlot> FindMatch(IEnumerable<ItemSlot> search, IReadOnlyCollection<Pattern> draw)
    {
        var matchList = new List<ItemSlot>();
        foreach (var slot in search)
        {
            if (matchList.Count >= itemHandler.pickupAtOnce) continue;
            if (NotPickable(slot)) continue;

            if (IsMatch(slot, draw))
            {
                matchList.Add(slot);
            }
        }

        return matchList;
    }

    bool NotPickable(ItemSlot slot)
        => slot.IsEmpty || itemHandler.moving.Contains(slot);

    bool NotMatch(ItemSlot item, IReadOnlyCollection<Pattern> draw) => !IsMatch(item, draw);
    bool IsMatch(ItemSlot item, IReadOnlyCollection<Pattern> draw)
    {
        if (draw.Count != item.PatternsCount) return false;
        var matched = draw.Count
        (d =>
            item.Patterns.Any
            (line =>
                d.start == line.start && d.end == line.end ||
                d.start == line.end && d.end == line.start));
        return matched == draw.Count;
    }
}