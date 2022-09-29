using UnityEngine;

namespace TabsUI
{
    public class TabCanvas : MonoBehaviour
    {
        [SerializeField] Tab parentTab;
        [SerializeField] Canvas canvas;

        void Awake()
        {
            Disable();
            if (!parentTab) return;
            parentTab.OnTabEnabled += Enable;
            parentTab.OnTabDisabled += Disable;
        }

        void Disable()
        {
            if (canvas)
                canvas.enabled = false;
        }

        void Enable()
        {
            if (canvas)
                canvas.enabled = true;
        }
    }
}