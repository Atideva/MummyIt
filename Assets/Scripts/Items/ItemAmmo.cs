using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemAmmo : Item
    {
        [SerializeField] AmmoConfig ammo;
        public void Set(AmmoConfig newAmmo) => ammo = newAmmo;
        public override Sprite Icon => ammo.Icon;
        public override string Name => ammo.name;
        public override IReadOnlyList<Pattern> Patterns => ammo.Patterns;

        public AmmoConfig Ammo => ammo;

        public override void Use()
        {
            Events.Instance.AddAmmo(ammo);
            ReturnToPool();
        }
    }
}