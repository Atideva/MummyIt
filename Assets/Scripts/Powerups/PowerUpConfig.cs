using System.Collections.Generic;
using AudioSystem;
using UnityEngine;

namespace Powerups
{
    [CreateAssetMenu(fileName = "PowerUp", menuName = "Configs/New PowerUp")]
    public class PowerUpConfig : ScriptableObject
    {
        [SerializeField] AudioData sound;
        [SerializeField] Sprite icon;
        [SerializeField] List<Pattern> patterns = new();
        [SerializeField] PowerUp prefab;
        public IReadOnlyList<Pattern> Patterns => patterns;
        public Sprite Icon => icon;

        public PowerUp Prefab => prefab;

        public AudioData Sound => sound;
    }
}