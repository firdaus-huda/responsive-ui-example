using DG.Tweening;
using StairwayGamesTest.UI.Input;
using UnityEngine;
using UnityEngine.UI;

namespace StairwayGamesTest.UI
{
    public class MainMenuButtonView : ButtonView
    {
        [SerializeField] private Image hoverFrame;

        private Tween _hoverTween;

        private const float AnimationDuration = 0.2f;

        public override void OnPointerEnter()
        {
            _hoverTween?.Kill();
            _hoverTween = hoverFrame.DOFade(1f, AnimationDuration);
        }
        
        public override void OnPointerExit()
        {
            _hoverTween?.Kill();
            _hoverTween = hoverFrame.DOFade(0f, AnimationDuration);
        }

        protected override void ResetView()
        {
            _hoverTween?.Kill();
            hoverFrame.color = new Color(hoverFrame.color.r, hoverFrame.color.g, hoverFrame.color.b, 0f);
        }
    }
}