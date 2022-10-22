using Pools;
using UnityEngine;

public class DrawGem : PoolObject
{
    [SerializeField] SpriteRenderer commonSprite;
    [SerializeField] SpriteRenderer highlightSprite;

    public void EnableHighlight()
    {
        commonSprite.enabled = false;
        highlightSprite.enabled = true;
    }

    public void DisableHighlight()
    {
        commonSprite.enabled = true;
        highlightSprite.enabled = false;
    }
}