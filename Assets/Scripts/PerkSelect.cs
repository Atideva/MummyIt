using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PerkSelect : MonoBehaviour
{
    public PlayerLevel playerLevel;
    public PerksSelectUI ui;
    public List<PerkConfig> perks = new();
    [Header("Guns")]
    public float gunChance = 0.2f;
    public List<PerkConfig> guns = new();
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
        }
        else
        {
            var pk = Instantiate(perk.Prefab, transform);
            pk.Activate();
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
        var perkList = perks.ToList();

        List<PerkConfig> choose = new();
        List<int> levels = new();
        
        for (var i = 0; i < 3; i++)
        {
            var r = Random.Range(0, perkList.Count);
            var p = perkList[r];
            choose.Add(p);
            var exist = chosenPerks.FirstOrDefault(perk => perk.config = p);
            var lvl = exist ? exist.level : 1;
            levels.Add(lvl);
            perkList.Remove(perkList[r]);
        }

        ui.Show(choose, levels);
    }
    
}