using UnityEngine;


[CreateAssetMenu(fileName = "Perk", menuName = "Configs/New Perk")]
public class PerkConfig : ScriptableObject
{
    [SerializeField] Sprite icon;
    [SerializeField] Perk prefab;
    [SerializeField][TextArea] string description;
    public Sprite Icon => icon;
    public Perk Prefab => prefab;
    public string Description => description;
    
}