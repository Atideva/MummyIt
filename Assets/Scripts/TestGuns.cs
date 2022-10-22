using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGuns : MonoBehaviour
{
    [Header("Create")]
    public EnemySpawner spawner;
    public Gun testGun;
    public EnemyConfig dummy;
    public List<GunConfig> guns = new();
   


    bool _created;
    Enemy _dummy;
    int _id;

    void Awake()
    {
        _id = -1;
    }

    [EButton.BeginVertical("TEST"),EButton  ]
    void Next()
    {
        _id++;
        if (_id >= guns.Count)
            _id = 0;
        ChangeGun();
    }
    [EButton, EButton.EndVertical]
    void Back()
    {
 
        _id--;
        if (_id < 0)
            _id = guns.Count - 1;
        ChangeGun();
    }
    
    // [Header("Create")]
    // public bool current;
    // public bool next;
    // public bool previous;
    void Update()
    {
        if (testGun.gun)
        {
            CreateDummy();
            testGun.ShootAtTarget(_dummy);
        }
        //
        // if (current)
        // {
        //     current = false;
        //     ChangeGun();
        // }
        //
        // if (next)
        // {
        //     next = false;
        //     _id++;
        //     if (_id >= guns.Count)
        //         _id = 0;
        //     ChangeGun();
        // }
        //
        // if (previous)
        // {
        //     previous = false;
        //     _id--;
        //     if (_id < 0)
        //         _id = guns.Count - 1;
        //     ChangeGun();
        // }
        
    }
    
    void CreateDummy()
    {
        if (!_dummy)
        {
            _dummy = Instantiate(dummy.prefab, new Vector3(0, 3.7f, 0), Quaternion.identity);
            _dummy.SetConfig(dummy);
            _dummy.SetImmune(999);
            spawner.AddToList(_dummy);
        }
    }

    void ChangeGun()
        => testGun.Change(guns[_id]);
}