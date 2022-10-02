using UnityEngine;


[CreateAssetMenu(fileName = "Perk", menuName = "Configs/New Perk")]
public class PerkConfig : ScriptableObject
{
    [SerializeField] Sprite icon;
    [SerializeField] string description;
    [SerializeField] Perk prefab;
    public Sprite Icon => icon;
    public Perk Prefab => prefab;
    public string Description => description;
    
}