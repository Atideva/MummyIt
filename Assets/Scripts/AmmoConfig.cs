using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ammo", menuName = "Configs/New Ammo")]
public class AmmoConfig : ScriptableObject
{
    [SerializeField] Sprite icon;
    [SerializeField] int amount = 1;
    [SerializeField] List<Pattern> patterns = new();

    public IReadOnlyList<Pattern> Patterns => patterns;
    public Sprite Icon => icon;
    public int Amount => amount;
}