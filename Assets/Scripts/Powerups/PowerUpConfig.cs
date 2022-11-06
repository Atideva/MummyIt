using System.Collections.Generic;
using AttackModificators;
using AudioSystem;
using UnityEngine;

namespace Powerups
{
    [CreateAssetMenu(fileName = "PowerUp", menuName = "Configs/New PowerUp")]
    public class PowerUpConfig : ScriptableObject
    {
        [SerializeField] PowerupType type;
        [SerializeField] AudioData pickupSound;
        [SerializeField] AudioData useSound;
        [SerializeField] Sprite icon;
        [SerializeField] List<Pattern> patterns = new();
        [SerializeField] PowerUp prefab;
        [SerializeField] VFX vfx;
        public IReadOnlyList<Pattern> Patterns => patterns;
        public Sprite Icon => icon;

        public PowerUp Prefab => prefab;

        public AudioData PickupSound => pickupSound;

        public PowerupType Type => type;

        public VFX Vfx => vfx;

        public AudioData UseSound => useSound;
    }
}

[System.Serializable]
public enum PowerupType
{
    Booster,
    Skill
}