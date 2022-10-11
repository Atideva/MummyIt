using System.Collections.Generic;
using System.Linq;
using Perks;
using Unity.VisualScripting;
using UnityEngine;

public class PerkSelect : MonoBehaviour
{
    public Player player;
    public PlayerLevel playerLevel;
    public PerksSelectUI ui;
    public int slotsCount = 2;
    public List<PerkConfig> perks = new();
    [Header("Guns")]
    public float gunChance = 0.2f;
    public List<GunConfig> guns = new();
    //  public List<AbilityConfig> abilitiesPool = new();
    [Header("DEBUG")]
    public List<Perk> ownedPerks = new();
    public int levelUps;

    void Start()
    {
        playerLevel.OnLevelUp += OnLevelUp;
        ui.OnPerkSelect += OnPerkSelect;
        ui.OnGunSelect += OnGunSelect;
        // ui.OnAbilitySelected += Continue;
    }

    void OnGunSelect(GunConfig gunConfig)
    {
        Events.Instance.GunPickup(gunConfig);
        CheckLevels();
    }

    public OwnedPerks ownedPerksUI;
    //  void Continue(AbilityConfig ab) 
    //      => Continue();

    void OnPerkSelect(PerkConfig perk)
    {
        Perk pk;
        pk = ownedPerks.Find(p => p.config == perk);
        //ownedPerks.Any(p => p.config == perk)
        if (pk)
        {
           // pk = ownedPerks.Find(p => p.config == perk);
          //  pk.LevelUp();
            Debug.LogError("OLD PERK, level: " + pk.level);
            Debug.LogError( "NewPerk: "+perk.name+", WonedPerk: " + pk.config.name);
        }
        else
        {
            pk = Instantiate(perk.Prefab, transform);
            pk.Init(perk);
            Debug.LogError("NEW PERK, level: " + pk.level);
            ownedPerks.Add(pk);
            //pk.Activate();
        }

        ownedPerksUI.PerkSelect(perk, pk.level);
        CheckLevels();
    }

    void CheckLevels()
    {
        levelUps--;
        if (levelUps == 0)
            Continue();
        else
        {
            if (perks.Count > 0)
                ChoosePerk();
            else
            {
                levelUps = 0;
                Continue();
            }
        }
    }

    void Continue() => Time.timeScale = 1;
    void Pause() => Time.timeScale = 0;

    void OnLevelUp(int lvl)
    {
        levelUps++;
        ChoosePerk();
    }

    void ChoosePerk()
    {
        if (perks.Count == 0) return;
        Show();
        Pause();
    }


    void Show()
    {
        var perkList = perks.ToList();

        List<PerkConfig> showPerks = new();
        List<int> levels = new();

        var from = 0;
        var isGunChance = Random.Range(0f, 1f) <= gunChance;
        GunConfig gun = null;
        var first = player.firstGun;
        var second = player.secondGun;

        if (isGunChance)
        {
            // ReSharper disable once InconsistentNaming
            var GUNS_LIST = new List<GunConfig>();

            var canUpgrade = first.CanUpgrade || second.CanUpgrade;

            //TODO: WHEN COMPARE NULL == NULL WEAPON?

            if (canUpgrade)
            {
                var onlyPlayerGuns = first.ChangeDisabled && second.ChangeDisabled;

                if (onlyPlayerGuns)
                {
                    Debug.Log("GunSelect: only player guns");
                    var playerGuns = new List<GunConfig>();
                    if (first.CanUpgrade)
                        playerGuns.Add(first.gun);
                    if (second.CanUpgrade)
                        playerGuns.Add(second.gun);
                    GUNS_LIST = playerGuns;
                }
                else
                {
                    if (first.gun == second.gun)
                    {
                        Debug.Log("GunSelect: same guns upgrade");
                        GUNS_LIST.Add(first.gun);
                    }
                    else
                    {
                        var gunsExclude = new List<GunConfig>();
                        if (first.IsMaxed)
                            gunsExclude.Add(first.gun);
                        if (second.IsMaxed)
                            gunsExclude.Add(second.gun);

                        Debug.Log("GunSelect: all guns mode. Excluded: " + gunsExclude.Count);
                        GUNS_LIST = guns.ToList();
                        foreach (var gunConfig in gunsExclude.Where(g => GUNS_LIST.Contains(g)))
                            GUNS_LIST.Remove(gunConfig);
                    }
                }
            }


            if (GUNS_LIST.Count > 0)
            {
                var random = Random.Range(0, GUNS_LIST.Count);
                gun = GUNS_LIST[random];
                var lvl =
                    first.gun == gun
                        ? first.lvl
                        : second.gun == gun
                            ? second.lvl
                            : 1;
                levels.Add(lvl);
                from++;
            }
        }

        for (var i = from; i < slotsCount; i++)
        {
            var r = Random.Range(0, perkList.Count);
            var perk = perkList[r];
            showPerks.Add(perk);
         //   var ownPerk = ownedPerks.Find(p => p.config = perk);
         var lvl = 1; //ownPerk ? ownPerk.level : 1;
            levels.Add(lvl);
            perkList.Remove(perkList[r]);
        }

        ui.Show(gun, showPerks, levels);
    }
}