using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Configs/New Ability")]
public class AbilityConfig : ScriptableObject
{
    [SerializeField] Sprite icon;
    public Sprite Icon => icon;
}
