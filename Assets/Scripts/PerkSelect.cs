using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PerkSelect : MonoBehaviour
{
    public PlayerLevel playerLevel;
    public PerksSelectUI ui;
    public List<PerkConfig> perks = new();
    public List<AbilityConfig> abilitiesPool = new();
    [Header("DEBUG")]
    public List<Perk> chosenPerks = new();
    void Start()
    {
        playerLevel.OnLevelUp += ChoosePerk;
        ui.OnPerkSelect += OnPerkSelect;
       // ui.OnAbilitySelected += Continue;
    }

  //  void Continue(AbilityConfig ab) 
  //      => Continue();

    void OnPerkSelect(PerkConfig perk)
    {
        if (chosenPerks.Any(p => p.config == perk))
        {
            var pk = chosenPerks.Find(p => p.config == perk);
            pk.LevelUp();
            //pk.Activate();
        }
        else
        {
            var pk = Instantiate(perk.Prefab, transform);
            chosenPerks.Add(pk);
            //pk.Activate();
        }
        Continue();
    }

    void Continue() => Time.timeScale = 1;
    void Pause() => Time.timeScale = 0;

    void ChoosePerk(int lvl)
    {
        if (perks.Count == 0) return;
        Show();
        Pause();
    }

    void Show()
    {
        var perks = this.perks.ToList();

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