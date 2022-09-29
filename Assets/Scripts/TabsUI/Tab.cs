using System;
using UnityEngine;
using UnityEngine.UI;

namespace TabsUI
{
    [ExecuteInEditMode]
    public class Tab : MonoBehaviour
    {
        [SerializeField]  Button button;
        public event Action OnTabEnabled = delegate { };
        public event Action OnTabDisabled = delegate { };
        public event Action<Tab> OnTabClicked = delegate { };
        public bool Enabled { get; private set; }
        void Awake() => button.onClick.AddListener(Clicked);
        
        public void ForceClick() => Clicked();
        
        public void Clicked() => OnTabClicked(this);

        public void Enable()
        {
            Enabled = true;
            OnTabEnabled();
        }

        public void Disable()
        {
            Enabled = false;
            OnTabDisabled();
        }
    }
}