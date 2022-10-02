using System.Collections.Generic;
using Powerups;
using UnityEngine;

namespace Items
{
    public class ItemPowerUp : Item
    {
        [SerializeField] PowerUpConfig powerUp;
        public void Set(PowerUpConfig newPowerUp) => powerUp = newPowerUp;

        public override Sprite Icon => powerUp.Icon;
        public override string Name => powerUp.name;
        public override IReadOnlyList<Pattern> Patterns => powerUp.Patterns;

        public override void Use()
        {
            Events.Instance.UsePowerUp(powerUp);
            ReturnToPool();
        }
    }
}