 

public class LazyPicker : Perk
{
    public int lazyCount;
    public int lazyIncPerLvl;
    public float cooldown;
    public float cooldownIncPerLvl;

    public override void Activate()
    {
        Events.Instance.AddLazyPicker(lazyCount, cooldown);
    }
}