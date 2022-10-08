namespace Perks
{
    public class AmmoPickupMaster : Perk
    {
        public int bonusItemPerLvl;
  
        public override void Activate()
        {
            Events.Instance.AddAmmoPickup(bonusItemPerLvl);
        }
    }
}
