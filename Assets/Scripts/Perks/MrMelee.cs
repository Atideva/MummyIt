using System.Collections.Generic;

namespace Perks
{
    public class MrMelee : Perk
    {
 
        public List<MeleeWeaponConfig> weapons = new();
 
    
        //public PlayerMeleeData incPerLvl;
 
        public override void Activate()
        {
            Events.Instance.PlayerMeleeWeapon(weapons[0]);
        }
    }
}
 