using System.Collections.Generic;
using UnityEngine;

namespace TabsUI
{
    public class TabsController : MonoBehaviour
    {
        [Header("Enable")]
        [SerializeField] bool enableOnStart;
        [Header("Sound")]
        //  [SerializeField] SoundData clickSound;
        [Header("Setup")]
        [SerializeField] Tab parentTab;
        [SerializeField] Tab firstOpenedTab;
        [SerializeField] Tab backToTab;
        [SerializeField] List<Tab> childTabs = new();
        Tab _lastTab;

        void Start()
        {
            SubscribeParent();
            SubscribeChild();
            OpenStartTab();
        }

        void SubscribeParent()
        {
            if (!parentTab) return;
            parentTab.OnTabEnabled += Enable;
            parentTab.OnTabDisabled += Disable;
        }

        void SubscribeChild()
        {
            foreach (var item in childTabs)
                item.OnTabClicked += TabClicked;
        }

        void Enable()
            => OpenStartTab();

        void Disable()
            => CloseLastTab();

        public void CloseTab()=>   backToTab.ForceClick();
        void TabClicked(Tab tab)
        {
            //    PlaySound(clickSound);
            if (tab.Enabled)
            {
                CloseTab();
            }
            else
                RefreshTabs(tab);
        }

        void RefreshTabs(Tab activeTab)
        {
            foreach (var tab in childTabs)
            {
                if (activeTab == tab)
                {
                    _lastTab = tab;
                    tab.Enable();
                }
                else
                    tab.Disable();
            }
        }

        void OpenStartTab()
        {
            if (firstOpenedTab)
                RefreshTabs(firstOpenedTab);
            else
            {
                if (childTabs[0])
                    RefreshTabs(childTabs[0]);
                else
                    Debug.LogError("myTabs.Count = 0");
            }
        }

        void CloseLastTab()
        {
            if (_lastTab)
                _lastTab.Disable();
        }

        //   void PlaySound(SoundData sound) => AudioManager.Instance.PlaySound(sound);
    }
}