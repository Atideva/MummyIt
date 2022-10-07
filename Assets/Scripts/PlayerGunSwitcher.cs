using UnityEngine;

public class PlayerGunSwitcher : MonoBehaviour
{
    public GunSlotsUI slotsUI;
   
    void Start()
    {
        slotsUI.OnSlotChange += OnSlotChange;
    }

    void OnSlotChange(GunSlot slot)
    {
         
    }
    
}
