using UnityEngine;
using UnityEngine.UI;

public class ManualShoot : MonoBehaviour
{
    public ManualShootTrigger trigger;
    public Gun gun;
    public Button shootButton;
    public Image triggerImage;
    public bool pressed;

    void Awake()
    {
        triggerImage.color = Color.clear;
        shootButton.onClick.AddListener(ForceShoot);
        trigger.OnPointUp += OnUp;
        trigger.OnPointDown += OnDown;
    }

    void ForceShoot()
        => gun.ShootAtClosestTarget();

    void OnDown()
        => pressed = true;
    void OnUp()
        => pressed = false;

    void FixedUpdate()
    {
        if (!pressed) return;
        ForceShoot();
    }
}