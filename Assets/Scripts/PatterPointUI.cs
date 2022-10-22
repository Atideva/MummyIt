using UnityEngine;
using UnityEngine.UI;

public class PatterPointUI : MonoBehaviour
{
    public Image circle;
    public Sprite activeSprite;
    public Sprite disabledSprite;
    
    public float selectedSize = 1.2f;
    public void Enable()
    {
        circle.sprite = activeSprite;
        circle.transform.localScale = new Vector3(selectedSize, selectedSize, selectedSize);
    }

    public void Disable()
    {
        circle.sprite = disabledSprite;
        circle.transform.localScale = Vector3.one;
    }
}
