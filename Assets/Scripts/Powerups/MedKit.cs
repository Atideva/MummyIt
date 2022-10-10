namespace Powerups
{
    public class MedKit : PowerUp
    {
        public float hpRestore;

        protected override void OnUse()
        {
        Events.Instance.HealPlayer(hpRestore);
        }
    }
}