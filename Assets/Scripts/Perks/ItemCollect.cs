namespace Perks
{
    public class ItemCollect : Perk
    {
        public override void Activate()
        {
            Events.Instance.AddItemCollectSlot(1);
        }
    }
}
