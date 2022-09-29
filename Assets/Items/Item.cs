using System.Collections.Generic;
using Pools;
using UnityEngine;

namespace Items
{
    public abstract class Item : PoolObject
    {
        public Color TypeColor { get; private set; }
        public void SetColor(Color clr) => TypeColor = clr;
        
        public abstract string Name { get;}
        public abstract IReadOnlyList<Pattern> Patterns { get; }
        public abstract void Use();
    }
}