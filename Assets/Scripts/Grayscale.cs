using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grayscale : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] sprites;
    List<Color> colors=new();
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material grayMaterial;
    [SerializeField] Color defaultColor;
    [SerializeField] Color grayColor;
    bool _init;

    void Init()
    {
        _init = true;
        sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (var spr in sprites)
        {
            colors.Add(spr.color);
        }
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

        for (var i = 0; i < sprites.Length; i++)
        {
            var sprite = sprites[i];
            sprite.material = defaultMaterial;
            sprite.color = colors[i];
            //  sprite.color = defaultColor;
        }
    }
}