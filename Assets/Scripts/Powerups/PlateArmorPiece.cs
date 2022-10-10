namespace Powerups
{
    public class PlateArmorPiece : PowerUp
    {
        protected override void OnUse()
        {
            Events.Instance.RestorePlateArmor();
        }
    }
}