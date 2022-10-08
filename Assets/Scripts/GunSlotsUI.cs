using System.Collections.Generic;
using UnityEngine;

public class GunSlotsUI : MonoBehaviour
{
    public GunSlot firstSlot;
    public GunSlot secondSlot;
    [Header("Rank Icons")]
    public List<Sprite> ranks = new List<Sprite>();
    Sprite RankSprite(int lvl) => lvl < ranks.Count ? ranks[lvl] : ranks[^1];
    void Start()
    {
        //firstSlot.OnClick += OnSlotClick;
        // secondSlot.OnClick += OnSlotClick;
    }

    public void FirstSlotRefresh(Gun playerGun)
    {
        firstSlot.SetGun(playerGun.gun);
        
        if (playerGun.attackModificator)
            firstSlot.SetModificator(playerGun.attackModificator.Icon);
        else
            firstSlot.DisableModificator();

        var rankSprite = RankSprite(playerGun.lvl);
        firstSlot.SetRank(rankSprite);
    }

    public void SecondSlotRefresh(Gun playerGun)
    {
        secondSlot.SetGun(playerGun.gun);
        
        if (playerGun.attackModificator)
            secondSlot.SetModificator(playerGun.attackModificator.Icon);
        else
            secondSlot.DisableModificator();

        var rankSprite = RankSprite(playerGun.lvl);
        secondSlot.SetRank(rankSprite);
    }

    void OnSlotClick(GunSlot slot)
    {
    }
}