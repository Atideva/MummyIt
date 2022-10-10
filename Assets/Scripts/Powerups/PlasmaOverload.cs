namespace Powerups
{
    public class PlasmaOverload : PowerUp
    {
        public PlasmaOverloadData settings;

        protected override void OnUse()
        {
            Events.Instance.UsePlasmaOverload(settings);
            ReturnToPool();
        }
    }
}