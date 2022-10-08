using UnityEngine;

namespace AttackModificators
{
    [CreateAssetMenu(fileName = "Attack Modificator", menuName = "Configs/New Attack Modificator")]
    public class AttackModificatorConfig : ScriptableObject
    {
        public Sprite icon;
        [Header("VFX")]
        public AttackModificatorVFX vfxPrefab;
        [Header("Slow")]
        [SerializeField][Range(0,1)] float speedSlow;
        [SerializeField]  float armorDecrease;
    
        [Header("DOT")]
        [SerializeField]  float damageOverTime;
        [SerializeField]  float damageOverTimeDuration;


        public float ArmorDecrease => armorDecrease;
        public float SpeedSlow => speedSlow;
        public float DamageOverTime => damageOverTime;
        public float DamageOverTimeDuration => damageOverTimeDuration;

        public AttackModificatorVFX VFXPrefab => vfxPrefab;

        public Sprite Icon => icon;
    }
}