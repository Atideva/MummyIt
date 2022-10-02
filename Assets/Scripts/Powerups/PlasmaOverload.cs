namespace Powerups
{
    public class PlasmaOverload : PowerUp
    {
        public PlasmaOverloadData settings;
        public override void Use()
        {
            Events.Instance.UsePlasmaOverload(settings);
            ReturnToPool();
        }
    }
}