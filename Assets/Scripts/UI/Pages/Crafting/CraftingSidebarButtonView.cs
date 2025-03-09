using DG.Tweening;
using StairwayGamesTest.Common;
using StairwayGamesTest.UI.Input;
using UnityEngine;
using UnityEngine.UI;

namespace StairwayGamesTest.UI.Pages.Crafting
{
    public class CraftingSidebarButtonView : ButtonView
    {
        [SerializeField] private Image hoverFrame;
        [SerializeField] private Image background;
        [SerializeField] private GameObject titleText;

        private const float AnimationDuration = 0.2f;

        private bool _isSelected;
        private Tween _hoverFrameTween;

        public override void OnPointerEnter()
        {
            _hoverFrameTween?.Kill();
            _hoverFrameTween = hoverFrame.DOFade(1f, AnimationDuration);
        }

        public override void OnPointerExit()
        {
            _hoverFrameTween?.Kill();
            _hoverFrameTween = hoverFrame.DOFade(0f, 0f);

            if (!_isSelected)
            {
                background.DOFade(0f, AnimationDuration);
            }
        }

        public override void OnPointerDown()
        {
            background.DOFade(0.25f, AnimationDuration);
        }

        public void SetSelected(bool selected)
        {
            if (selected)
            {
                background.DOFade(1f, AnimationDuration);
                titleText.SetActive(true);
                
                AudioEngine.PlaySfx("switch29");
            }
            else
            {
                if (_isSelected)
                {
                    background.DOFade(0f, 0.01f);
                    titleText.SetActive(false);
                }
            }

            _isSelected = selected;
        }

        protected override void ResetView()
        {
            _hoverFrameTween?.Kill();
            hoverFrame.color = new Color(hoverFrame.color.r, hoverFrame.color.g, hoverFrame.color.b, 0f);
        }
    }
}