
public class Engineer : Perk
{
    public float itemSpawnIncrease;
    public float powerupSpawnIncrease;
    
    public void Refresh()
    {
        Events.Instance.AddItemSpawnRate(itemSpawnIncrease);
        Events.Instance.AddPowerupSpawnRate(powerupSpawnIncrease);
    }
    
}
