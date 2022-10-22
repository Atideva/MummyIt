using System.Collections.Generic;
using Pools;
using UnityEngine;

namespace Items
{
    public abstract class Item : PoolObject
    {
        public int sellPrice = 10;
        public abstract Sprite Icon { get;    }
     //   public void SetIcon(Sprite spr) => Icon = spr;
      //  public Color TypeColor { get; private set; }
      //  public void SetColor(Color clr) => TypeColor = clr;
      public Sprite TypeSprite { get; private set; }
      public void SetTypeSprite(Sprite spr) => TypeSprite = spr;
        
        public abstract string Name { get;}
        public abstract IReadOnlyList<Pattern> Patterns { get; }
        public abstract void Use();
    }
}