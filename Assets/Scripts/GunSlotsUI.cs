using System.Collections.Generic;
using UnityEngine;

public class GunSlotsUI : MonoBehaviour
{
    public GunSlot firstSlot;
    public GunSlot secondSlot;
    [Header("Rank Icons")]
    public List<Sprite> ranks = new List<Sprite>();

    Sprite RankSprite(int lvl)
        => lvl - 1 < ranks.Count
            ? ranks[lvl - 1]
            : ranks[^1];

    void Start()
    {
        firstSlot.Enable();
        secondSlot.Disable();

        firstSlot.DisableRank();
        secondSlot.DisableRank();
        //firstSlot.OnClick += OnSlotClick;
        // secondSlot.OnClick += OnSlotClick;
    }

    public void FirstSlotRefresh(Gun playerGun)
    {
        firstSlot.Enable();
        firstSlot.SetGun(playerGun.gun);

        if (playerGun.attackModificator)
            firstSlot.SetModificator(playerGun.attackModificator.Icon);
        else
            firstSlot.DisableModificator();

        if (playerGun.lvl > 1)
        {
            var rankSprite = RankSprite(playerGun.lvl);
            firstSlot.SetRank(rankSprite);
        }
    }

    public void SecondSlotRefresh(Gun playerGun)
    {
        secondSlot.Enable();
        secondSlot.SetGun(playerGun.gun);

        if (playerGun.attackModificator)
            secondSlot.SetModificator(playerGun.attackModificator.Icon);
        else
            secondSlot.DisableModificator();

        if (playerGun.lvl > 1)
        {
            var rankSprite = RankSprite(playerGun.lvl);
            secondSlot.SetRank(rankSprite);
        }
    }

    void OnSlotClick(GunSlot slot)
    {
    }
}