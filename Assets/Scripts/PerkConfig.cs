using UnityEngine;


[CreateAssetMenu(fileName = "Perk", menuName = "Configs/New Perk")]
public class PerkConfig : ScriptableObject
{
    [SerializeField] Sprite icon;
    public Sprite Icon => icon;
}