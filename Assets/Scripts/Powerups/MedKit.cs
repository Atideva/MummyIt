namespace Powerups
{
    public class MedKit : PowerUp
    {
        public float hpRestore;
        public override void Use()
        {
        Events.Instance.HealPlayer(hpRestore);
        }
    }
}