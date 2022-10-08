namespace Perks
{
    public class TakeAim : Perk
    {
        public float bulletSpdBonus;
        public float bulletDmgBonus;
 
        public override void Activate()
        {
            Events.Instance.TakeAim();
            Events.Instance.AddBulletSpeed(bulletSpdBonus);
            Events.Instance.AddBulletDamage(bulletDmgBonus);
        }
    }
}