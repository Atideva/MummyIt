using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerPlateArmor : MonoBehaviour
{
    [System.Serializable]
    public class Plate
    {
        public float Durability;
        public float MaxDurability;
        public float Percent => Durability / MaxDurability;
        public void Restore()
            => Durability = MaxDurability;
        public void Damage(float dmg)
        {
            Durability -= dmg;
            if (Durability < 0) Durability = 0;
        }

        public Plate(float max)
        {
            MaxDurability = max;
            Durability = max;
        }
    }

    public PlateArmorListUI ui;
    public List<Plate> plates = new();

    bool IsEnabled
        => plates.Count > 0;

    float Durability
        => plates.Sum(plate => plate.Durability);

    public bool IsAny
        => IsEnabled && Durability > 0;

    public void Damage(float dmg)
    {
        for (var i = plates.Count - 1; i >= 0; i--)
        {
            var receiver = plates[i];
            if (receiver.Durability <= 0) continue;
            receiver.Damage(dmg);
            break;
        }
    }

    void Start()
    {
        ui.DisableAll();
        Events.Instance.OnPlateArmorAdd += OnAdd;
    }

    void OnAdd(float durability)
    {
        for (var i = 0; i < plates.Count; i++)
        {
            var plate = plates[i];
            plate.Restore();
            ui.Refresh(i, plate.Percent);
        }

        var newPlate = new Plate(durability);
        plates.Add(newPlate);
        ui.Enable(plates.Count - 1);
        ui.Refresh(plates.Count - 1, newPlate.Percent);
    }
}