using UnityEngine;

public class TestPatternPoint : MonoBehaviour
{
    public SpriteRenderer circle;
    public float selectedSize = 1.2f;
    public void Enable()
    {
        circle.color = Color.blue;
        circle.transform.localScale = new Vector3(selectedSize, selectedSize, selectedSize);
    }

    public void Disable()
    {
        circle.color = Color.white;
        circle.transform.localScale = Vector3.one;
    }
}