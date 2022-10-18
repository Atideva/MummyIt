using System.Collections.Generic;
using UnityEngine;

namespace __ENEMIES.abilities
{
    public class SpawnOnDeath : EnemyAbility
    {
        public List<EnemyConfig> spawnEnemies = new();
        public List<float> delays = new();
        public List<Vector3> offsets = new();

        protected override void OnInit()
        {
            Owner.hp.OnDeath += OnDeath;
            for (var i = 0; i < spawnEnemies.Count; i++)
            {
                if (delays.Count <= i)
                    delays.Add(0);

                if (offsets.Count <= i)
                    offsets.Add(Vector3.zero);
            }
        }

        void OnDeath()
        {
            for (var i = 0; i < spawnEnemies.Count; i++)
                Events.Instance.SpawnEnemy(spawnEnemies[i], transform.position + offsets[i], delays[i]);
        }

        public override void Reset()
        {
        }
    }
}