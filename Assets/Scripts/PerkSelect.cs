using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PerkSelect : MonoBehaviour
{
    public PlayerLevel playerLevel;
    public PerksSelectUI ui;
    public List<PerkConfig> perksPool = new();
    public List<AbilityConfig> abilitiesPool = new();

    void Start()
    {
        playerLevel.OnLevelUp += ChoosePerk;
        ui.OnPerkSelected += Continue;
        ui.OnAbilitySelected += Continue;
    }

    void Continue(AbilityConfig ab) 
        => Continue();

    void Continue(PerkConfig perk)
        => Continue();

    void Continue() => Time.timeScale = 1;
    void Pause() => Time.timeScale = 0;

    void ChoosePerk(int lvl)
    {
        if (perksPool.Count == 0) return;
        Show();
        Pause();
    }

    void Show()
    {
        var perks = perksPool.ToList();

        List<PerkConfig> choose = new();
        for (int i = 0; i < 3; i++)
        {
            var r = Random.Range(0, perks.Count);
            choose.Add(perks[r]);
            perks.Remove(perks[r]);
        }

        ui.Show(choose);
    }
}