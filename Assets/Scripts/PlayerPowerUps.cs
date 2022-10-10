 using System.Collections.Generic;
using System.Linq;
using Powerups;
using UnityEngine;

public class PlayerPowerUps : MonoBehaviour
{
    public List<PowerUpConfig> availablePowerUps = new();
    [SerializeField] AnimationCurve ammoCurve;
    [SerializeField] float chanceFactor = 4f;
    readonly Dictionary<PowerUpConfig, List<PowerUp>> _powerUpPool = new();

    void Start()
    {
        Events.Instance.OnUsePowerUp += OnPowerUpUse;
    }
    
    void OnPowerUpUse(PowerUpConfig powerUp)
    {
        PowerUp up;
        if (_powerUpPool.ContainsKey(powerUp))
        {
            var list = _powerUpPool[powerUp];
            if (list.Any(p => p.IsAvailable))
            {
                up = list.Find(p => p.IsAvailable);
            }
            else
            {
                up = Instantiate(powerUp.Prefab, transform);
                list.Add(up);
                //powerUpPool[powerUp] = list;
            }
        }
        else
        {
            var newList = new List<PowerUp>();
            up = Instantiate(powerUp.Prefab, transform);
            newList.Add(up);
            _powerUpPool.Add(powerUp, newList);
        }

       
        up.Use();
    }

    public float GetAmmoChance(int ammoID)
    {
        var point = availablePowerUps.Count > 1 ? (float) ammoID / (availablePowerUps.Count - 1) : 0;
        var value = ammoCurve.Evaluate(point);

        var factor = 1 / chanceFactor;
        var factorValue = value + factor;
        var factorTotal = TotalChance + factor * availablePowerUps.Count;

        return factorTotal > 0 ? factorValue / factorTotal : value;
    }

    float TotalChance => availablePowerUps
        .Select((t, i) => i / (float) (availablePowerUps.Count - 1))
        .Sum(ammoCurve.Evaluate);
    
}