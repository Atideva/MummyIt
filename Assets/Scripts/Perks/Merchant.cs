namespace Perks
{
    public class Merchant : Perk
    {
        public float sellMult;
        public float incPerLvlMult;
 
        public override void Activate()
        {
            Events.Instance.Merchant(sellMult);
        }
    }
}
