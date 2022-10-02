using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Powerups
{
    public class HammerSmashVfx : MonoBehaviour
    {
        [SerializeField] SpriteRenderer earthSplit;
        [SerializeField] List<DOTweenAnimation> animations = new();
        HammerSmash _damageScript;
        List<Enemy> _targets = new();
        float _timer;
        public BoxCollider2D col;
        public List<GameObject> vfxs = new();
        public float damageDelay = 0.7f;
        public float vfxDelay = 0.7f;
        public float hideEarthSplitDelay = 2f;
        public float hideEarthSplitDur = 2f;

        [Header("TEST")]
        public bool testAnim;

#if UNITY_EDITOR
        void Update()
        {
            if (testAnim)
            {
                testAnim = false;
                foreach (var anim in animations)
                {
                    anim.DORestart();
                    anim.DOPlay();
                }

                earthSplit.DOFade(0, hideEarthSplitDur).SetDelay(hideEarthSplitDelay);
                Invoke(nameof(PlayVfx), vfxDelay);
            }
        }
#endif

        public bool IsInit
            => _damageScript;

        public void Init(HammerSmash script, Vector2 pos)
        {
            _damageScript = script;
            transform.position = pos;
        }


        public void SmashThem()
        {
            gameObject.SetActive(true);
            _targets = new List<Enemy>();
            _timer = 0.1f;

            foreach (var anim in animations)
            {
                anim.DORestart();
                anim.DOPlay();
            }

            earthSplit.DOFade(0, hideEarthSplitDur).SetDelay(hideEarthSplitDelay);
            Invoke(nameof(PlayVfx), vfxDelay);
            Invoke(nameof(GetTargets), damageDelay);
        }

        void PlayVfx()
        {
            foreach (var vfx in vfxs)
                vfx.SetActive(true);
        }

        void GetTargets()
            => col.enabled = true;

        public void OnTriggerEnter2D(Collider2D collided)
        {
            if (!collided.CompareTag(Tags.ENEMY)) return;
            var enemy = EnemySpawner.Instance.TryFindEnemy(collided.transform);
            if (!enemy) return;
            if (_targets.Contains(enemy)) return;
            _targets.Add(enemy);
        }

        void Return()
        {
            gameObject.SetActive(false);
            _damageScript.Return();
        }

        void FixedUpdate()
        {
            if (!col.enabled) return;
            _timer -= Time.fixedDeltaTime;
            if (_timer > 0) return;
            _damageScript.DamageTargets(_targets);
            col.enabled = false;
            Invoke(nameof(Return), 2f);
        }
    }
}