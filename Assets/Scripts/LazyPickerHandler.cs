 
using UnityEngine;

public class LazyPickerHandler : MonoBehaviour
{
    public LazyPickerUI ui;
    [Header("DEBUG")]
    public bool isEnable;
    public int maxCount;
    public int current;
    public float timer;
    public float cooldown;
    public bool collectAnimation;
    float FillValue => timer / cooldown;
    
    void Awake()
    {
        ui.Disable();
    }
 
    void Start()
    {
        OnAdd(99999, 1);
        Events.Instance.OnLazyPickerAdd += OnAdd;
    }

    public void SpendOneCharge()
    {
        current--;
        ui.RefreshCount(current);
    }

    void OnAdd(int addMaxCount, float newCooldown)
    {
        if (!isEnable)
        {
            isEnable = true;
            current = addMaxCount;
        }

        maxCount += addMaxCount;
        cooldown = newCooldown;
    }


    void FixedUpdate()
    {
        if (!isEnable) return;
        if (current >= maxCount) return;
        if (collectAnimation) return;

        timer += Time.fixedDeltaTime;
        ui.RefreshCooldown(FillValue);

        if (timer < cooldown) return;

        collectAnimation = true;
        ui.CollectAnim();

        current++;
        ui.RefreshCount(current);
        Invoke(nameof(CollectAnimFinished), ui.AnimTime);
    }

    void CollectAnimFinished()
    {
        collectAnimation = false;
        timer = 0;
    }
}