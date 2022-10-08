using System.Collections.Generic;
using System.Linq;
using Pools;
using UnityEngine;

namespace AttackModificators
{
    public class AttackModificatorHandler : MonoBehaviour
    {
        [Header("DEBUG")]
        public List<DoT> dots = new();
        readonly Dictionary<AttackModificatorVFX, ModificatorVFXPool> _pools = new();

        void Start()
        {
            Events.Instance.OnApplyAttackModifier += OnApplyModifier;
            Events.Instance.OnEnemyDeath += OnEnemyDeath;
        }

        void FixedUpdate()
        {
            if (dots.Count > 0)
            {
                var remove = new List<DoT>();
                foreach (var dot in dots)
                {
                    var dmg = dot.Damage * (Time.fixedDeltaTime / dot.Duration);
                    dot.Enemy.Damage(dmg);
                    dot.Duration -= Time.fixedDeltaTime;
                    if (dot.Duration <= 0)
                        remove.Add(dot);
                }

                foreach (var dot in remove)
                    dots.Remove(dot);
            }
        }


        void OnApplyModifier(Enemy enemy, IReadOnlyList<AttackModificatorConfig> modifiers)
        {
            foreach (var mod in modifiers)
            {
                if (mod.DamageOverTime > 0)
                {
                    var dot = new DoT
                    {
                        Enemy = enemy,
                        Damage = mod.DamageOverTime,
                        Duration = mod.DamageOverTimeDuration
                    };
                    dots.Add(dot);
                }
            }
        }

        void OnEnemyDeath(Enemy enemy)
        {
            var remove = dots.Where(dot => dot.Enemy == enemy).ToList();
            foreach (var dot in remove)
                dots.Remove(dot);
        }

        ModificatorVFXPool Pool(AttackModificatorVFX enemy)
        {
            if (_pools.ContainsKey(enemy)) return _pools[enemy];

            var container = new GameObject {name = enemy.name};
            container.transform.SetParent(transform);

            var pool = container.AddComponent<ModificatorVFXPool>();
            pool.SetPrefab(enemy);

            _pools.Add(enemy, pool);
            return _pools[enemy];
        }
    }
}