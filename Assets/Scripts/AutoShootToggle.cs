using UnityEngine;
using UnityEngine.UI;

public class AutoShootToggle : MonoBehaviour
{
    public Gun firstGun;
    public Gun secondGun;
    public Button toggleButton;
    public Image toggleImage;
    public Color enableColor;
    public Color disableColor;
    bool _enable;

    void Start()
    {
        _enable = true;
        ChangeState(_enable);
        toggleButton.onClick.AddListener(Toggle);
    }


    void Toggle()
    {
        _enable = !_enable;
        ChangeState(_enable);
    }

    void ChangeState(bool isEnable)
    {
        if (isEnable)
        {
            firstGun.EnableAutoShoot();
            secondGun.EnableAutoShoot();
        }
        else
        {
            firstGun.DisableAutoShoot();
            secondGun.DisableAutoShoot();
        }
        
        toggleImage.color = isEnable ? enableColor : disableColor;
    }
}