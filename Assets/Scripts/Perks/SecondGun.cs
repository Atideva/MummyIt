namespace Perks
{
    public class SecondGun : Perk
    {
        public override void Activate()
        {
            Events.Instance.AllowSecondGun();
        }
    }
}