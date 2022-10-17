using System.Collections.Generic;
using AttackModificators;
using UnityEngine;

namespace __ENEMIES.abilities
{
    public class Reincarnate : EnemyAbility
    {
        
        public int resurrects = 1;
 
        public float delay = 1f;
        public VFX spawnGraveVfx;
        public VFX reincarnateVfx;
        
        public Transform graveContainer;
        public List<SpriteRenderer> grave = new();
        public List<SpriteRenderer> costume = new();
    

        protected override void OnInit()
        {
            foreach (var sprite in grave)
                sprite.enabled = false;
            graveContainer.transform.SetParent(null);
            Owner.hp.OnDeath += OnDeath;
        }

        public override void Reset()
        {
            foreach (var sprite in costume)
                sprite.enabled = true;
            foreach (var sprite in grave)
                sprite.enabled = false;
        }

        void OnDeath()
        {
            if (resurrects > 0)
            {
                Events.Instance.PlayVfx(spawnGraveVfx, Owner.transform.position);
                foreach (var sprite in grave)
                    sprite.enabled = true;
                graveContainer.transform.position = Owner.transform.position;
                Owner.gameObject.SetActive(false);
                Invoke(nameof(PlayResurrectVfx), delay - 0.3f);
                Invoke(nameof(Resurrect), delay);
            }
        }

        void PlayResurrectVfx()
        {
            Events.Instance.PlayVfx(reincarnateVfx, Owner.transform.position);
        }

        void Resurrect()
        {
            resurrects--;
            Owner.gameObject.SetActive(true);
            Owner.hp.HealAll();
            Owner.Move();
            EnemySpawner.Instance.AddToList(Owner);
            foreach (var sprite in grave)
                sprite.enabled = false;
            foreach (var sprite in costume)
                sprite.enabled = false;
        }
    }
}