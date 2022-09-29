using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemAmmo : Item
    {
        [SerializeField] AmmoConfig ammo;
        public void Set(AmmoConfig newAmmo) => ammo = newAmmo;

        public override string Name => ammo.name;
        public override IReadOnlyList<Pattern> Patterns => ammo.Patterns;

        public override void Use()
        {
            Events.Instance.AddAmmo(ammo);
            ReturnToPool();
        }
    }
}