 
using UnityEngine;
using UnityEngine.UI;

public class PlateArmorUI : MonoBehaviour
{
    public Image fill;
    
    public void Set(float value) 
        => fill.fillAmount = value;

    public void Enable() 
        => gameObject.SetActive(true);

    public void Disable()
        => gameObject.SetActive(false);
}