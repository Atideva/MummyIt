using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TabsUI
{
    public class TabUI : MonoBehaviour
    {
        [SerializeField] Tab onTab;
        [SerializeField] Image circleImage;
        [SerializeField] Image iconImage;
        [SerializeField] Sprite circleEnableSprite;
        [SerializeField] Sprite circleDisableSprite;
        [SerializeField] RectTransform container;
        [SerializeField] float containerMoveBy;
        [SerializeField] float containerMoveTime;
        [SerializeField] TextMeshProUGUI nameLabel;
        [SerializeField] List<Image> backgrounds = new();
        [SerializeField] Color backEnableColor;
        [SerializeField] Color backDisableColor;
        [SerializeField] List<Image> frames = new();
        [SerializeField] Color frameEnableColor;
        [SerializeField] Color frameDisableColor;
        bool _enable;
        bool _init;

        void Awake()
        {
            _enable = true;
            onTab.OnTabEnabled += Enable;
            onTab.OnTabDisabled += Disable;
            Invoke(nameof(Init), 1f);
        }


        void Init() => _init = true;

        void Enable()
        {
            if (_enable) return;
            _enable = true;
            if (circleEnableSprite) circleImage.sprite = circleEnableSprite;
            foreach (var b in backgrounds) b.color = backEnableColor;
            foreach (var f in frames) f.color = frameEnableColor;
            // nameLabel.enabled = true;
            nameLabel.color = Color.white;
            circleImage.color = backEnableColor;
            //   circleImage.transform.DOScale(1.4f, containerMoveTime / 2).OnComplete(()
            container.transform.DOScale(1.35f, containerMoveTime / 2).OnComplete(()
                => container.transform.DOScale(1.15f, containerMoveTime / 2));
            // iconImage.transform.DOScale(1.3f, containerMoveTime / 2).OnComplete(()
            //     => iconImage.transform.DOScale(1.2f, containerMoveTime / 2));
          //  iconImage.DOFade(1f, 0);
            if (_init)
                container.DOLocalMoveY(containerMoveBy, containerMoveTime).SetRelative(true);
            else
                container.anchoredPosition = new Vector2(0, containerMoveBy);
        }

        void Disable()
        {
            if (!_enable) return;
            _enable = false;
            if (circleDisableSprite) circleImage.sprite = circleDisableSprite;
            foreach (var b in backgrounds) b.color = backDisableColor;
            foreach (var f in frames) f.color = frameDisableColor;
            //  nameLabel.enabled = false;
            nameLabel.color = new Color(1, 1, 1, 0.66f);
            circleImage.color = backDisableColor;
            //   circleImage.transform.DOScale(1, containerMoveTime);
           // iconImage.transform.DOScale(0.9f, containerMoveTime);
         //  iconImage.DOFade(0.6f, 0);
            container.transform.DOScale(0.95f, containerMoveTime);
            if (_init)
                container.DOLocalMoveY(-containerMoveBy, containerMoveTime).SetRelative(true);
            else
                container.anchoredPosition = new Vector2(0, 0);
        }
    }
}