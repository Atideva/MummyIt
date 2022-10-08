namespace Perks
{
    public class AmmoMagnet : Perk
    {
        public float cooldown;
 
        public override void Activate()
        {
            Events.Instance.AmmoMagnet(cooldown);
        }
    }
}
