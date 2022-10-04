namespace Powerups
{
    public class PlateArmorPiece : PowerUp
    {
        public override void Use()
        {
            Events.Instance.RestorePlateArmor();
        }
    }
}