namespace Powerups
{
    public class LevelUp : PowerUp
    {
        protected override void OnUse()
        {
            Events.Instance.LevelUp();
            ReturnToPool();
        }
    }
}