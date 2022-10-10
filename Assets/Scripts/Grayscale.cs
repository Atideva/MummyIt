using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grayscale : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] sprites;
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material grayMaterial;
    [SerializeField] Color defaultColor;
    [SerializeField] Color grayColor;
    bool _init;
    void Init()
    {
        _init = true;
        sprites = GetComponentsInChildren<SpriteRenderer>();
    }

    bool _isGray;

    public void Enable()
    {
        if (_isGray) return;
        _isGray = true;

        if (!_init) Init();

        foreach (var sprite in sprites)
        {
            sprite.material = grayMaterial;
            sprite.color = grayColor;
        }
    }

    public void Disable()
    {
        if (!_isGray) return;
        _isGray = false;

        if (!_init) Init();

        foreach (var sprite in sprites)
        {
            sprite.material = defaultMaterial;
            sprite.color = defaultColor;
        }
    }
}