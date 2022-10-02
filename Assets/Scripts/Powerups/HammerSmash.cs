using System.Collections.Generic;
using UnityEngine;

namespace Powerups
{
    public class HammerSmash : PowerUp
    {
        public Vector2 spawnPos;
        public HammerSmashVfx vfx;
        public float damage;

        public void DamageTargets(List<Enemy> targets)
        {
            foreach (var target in targets)
                target.Damage(damage);
        }

        public void Return() => ReturnToPool();

        public override void Use()
        {
            if (!vfx.IsInit)
                vfx.Init(this, spawnPos);
            vfx.SmashThem();
        }
    }
}