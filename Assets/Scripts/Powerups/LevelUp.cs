namespace Powerups
{
    public class LevelUp : PowerUp
    {
        public override void Use()
        {
            Events.Instance.LevelUp();
            ReturnToPool();
        }
    }
}