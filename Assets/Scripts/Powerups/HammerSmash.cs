using System.Collections.Generic;
using UnityEngine;

namespace Powerups
{
    public class HammerSmash : PowerUp
    {
        public Vector2 spawnPos;
        public HammerSmashVfx vfx;
        public float damage;

        protected override void OnUse()
        {
            if (!vfx.IsInit)
                vfx.Init(this, spawnPos);
            vfx.SmashThem();
        }

        public void DamageTargets(List<Enemy> targets)
        {
            foreach (var target in targets)
                target.Damage(damage);
        }

        public void Return() => ReturnToPool();
    }
}