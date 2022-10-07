using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LazyPickerUI : MonoBehaviour
{
    public TextMeshProUGUI countText;
    public Image fillCooldown;
    public Transform countContainer;
    [Header("Anim")]
    public float pumpSize;
    public float pumpTime;
    public float AnimTime => pumpTime;
    
    public void RefreshCooldown(float value)
    {
        fillCooldown.fillAmount = value;
    }

    public void CollectAnim()
    {
        countContainer.DOScale(pumpSize, pumpTime / 2)
            .OnComplete(()
                => countContainer.DOScale(1, pumpTime / 2));
    }
    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void RefreshCount(int amount)
    {
        countText.text = amount.ToString();
    }
}