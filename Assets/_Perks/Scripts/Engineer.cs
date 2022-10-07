
public class Engineer : Perk
{
    public float itemSpawnIncrease;
    public float powerupSpawnIncrease;
    
 
    public override void Activate()
    {
        Events.Instance.AddItemSpawnRate(itemSpawnIncrease);
        Events.Instance.AddPowerupSpawnRate(powerupSpawnIncrease);
    }
}
