namespace Perks
{
    public class PlateArmor : Perk
    {
        public float onePlateDurability;
    
    

        public override void Activate()
        {
            Events.Instance.AddPlateArmor(onePlateDurability);
        }
    }
}
