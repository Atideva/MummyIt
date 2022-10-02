 
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpUI : MonoBehaviour
{
 
    public Image hpBar;

    public void RefreshBar(float value)
    {
        hpBar.fillAmount = value;
    }
     
}
