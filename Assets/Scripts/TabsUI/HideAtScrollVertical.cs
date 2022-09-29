using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace TabsUI
{
    public class HideAtScrollVertical : MonoBehaviour
    {
        public RectTransform tabContainer;
        public float hideTime;
        public float hideDistance;
        public ScrollRect scroll;
        public float minSpeedToHide;
        [Header("DEBUG")]
        public float scrollSpeed;
        bool _hide;
        bool _process;

        void FixedUpdate()
        {
            if (Time.time < 0.2f) return;
            scrollSpeed = scroll.velocity.magnitude;
            if (scrollSpeed > minSpeedToHide)
                Hide();
            else
                Show();
        }


        void Hide()
        {
            if (_process) return;
            if (_hide) return;
            _hide = true;
            _process = true;

            tabContainer
                .DOLocalMoveY(-hideDistance, hideTime)
                .SetRelative(true)
                .OnComplete(() => _process = false);
        }

        void Show()
        {
            if (_process) return;

            if (!_hide) return;
            _hide = false;
            _process = true;

            tabContainer
                .DOLocalMoveY(hideDistance, hideTime)
                //  .DOLocalMove(Vector3.zero, hideTime)
                .SetRelative(true)
                .OnComplete(() => _process = false);
        }
    }
}