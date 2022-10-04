using UnityEngine;

public abstract class Perk : MonoBehaviour
{
    [HideInInspector] public PerkConfig config;
    public int level;
    public int maxLevel;

//     public abstract void Activate();
    public void LevelUp()
    {
    }
}