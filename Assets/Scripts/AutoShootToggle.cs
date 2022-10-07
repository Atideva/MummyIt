using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoShootToggle : MonoBehaviour
{
    public Gun gun;
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
            gun.EnableAutoShoot();
        else
            gun.DisableAutoShoot();
        
        toggleImage.color = isEnable ? enableColor : disableColor;
    }
}