using UnityEngine;

namespace Perks
{
    public abstract class Perk : MonoBehaviour
    {
        public int maxLevel;
        [Header("DEBUG")]
        public PerkConfig config;
        public int level;

        public void Init(PerkConfig perkConfig)
        {
            Debug.LogError("SETED", this);
            config = perkConfig;
            level = 1;
            Activate();
        }

        public abstract void Activate();
 
        public void LevelUp()
        {
            level++;
            Activate();
        }
    }
}