 
using UI;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpUI : MonoBehaviour
{
    public CustomSlider hpSlider;
//    public Image hpBar;

    public void RefreshBar(float value)
    {
        hpSlider.SetValue(value);
        // hpBar.fillAmount = value;
    }
     
}
