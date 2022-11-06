using System.Collections.Generic;
using System.Linq;
using AudioSystem;
using UnityEngine;

namespace Powerups
{
    public class Freeze : PowerUp
    {
        public float duration;
        public GameObject vfxPrefab;
        [Header("DEBUG")]
        public List<GameObject> vfxes = new();
        List<Enemy> _enemies = new();
 
        protected override void OnUse()
        {
 
            Events.Instance.OnEnemyDeath += OnEnemyDeath;
            FreezeAll();
        }

        void Stop()
        {
            foreach (var enemy in _enemies)
                enemy.Freeze(false);
            foreach (var vfx in vfxes)
                vfx.SetActive(false);

            Events.Instance.OnEnemyDeath -= OnEnemyDeath;
            ReturnToPool();
        }

        void FreezeAll()
        {
            _enemies = EnemySpawner.Instance.currentEnemies.ToList();

            for (var i = 0; i < _enemies.Count; i++)
            {
                var enemy = _enemies[i];
                if(enemy.Immune) continue;
                
                enemy.Freeze(true);
                if (i < vfxes.Count)
                {
                    vfxes[i].SetActive(true);
                    vfxes[i].transform.position = enemy.transform.position;
                }
                else
                {
                    var vfx = Instantiate(vfxPrefab, transform);
                    vfx.transform.position = enemy.transform.position;
                    vfxes.Add(vfx);
                }
            }

            Invoke(nameof(Stop), duration);
        }

        void OnEnemyDeath(Enemy enemy)
        {
            if (_enemies.Contains(enemy))
            {
                var i = _enemies.IndexOf(enemy);
                if (i < vfxes.Count)
                {
                    vfxes[i].SetActive(false);
                    vfxes.RemoveAt(i);
                }

                _enemies.Remove(enemy);
            }
        }
    }
}