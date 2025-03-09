using System;
using DG.Tweening;
using StairwayGamesTest.Common;
using StairwayGamesTest.UI.Input;
using UnityEngine;
using UnityEngine.UI;

namespace StairwayGamesTest.UI
{
    public class TabNavButtonView : ButtonView
    {
        [SerializeField] private Image baseImage;
        [SerializeField] private Image hoverImage;
        private const float AnimationDuration = 0.2f;

        private Tween _scaleTween;
        private Tween _baseImageTween;
        private Tween _hoverImageTween;

        public override void OnPointerEnter()
        {
            _baseImageTween?.Kill();
            _baseImageTween = baseImage.DOFade(0f, AnimationDuration);
            
            hoverImage.gameObject.SetActive(true);
            _hoverImageTween?.Kill();
            _hoverImageTween = hoverImage.DOFade(1f, AnimationDuration);
        }
        
        public override void OnPointerExit()
        {
            _baseImageTween?.Kill();
            _baseImageTween = baseImage.DOFade(0.12f, AnimationDuration);
            
            _hoverImageTween?.Kill();
            _hoverImageTween = hoverImage.DOFade(0f, AnimationDuration);
            _hoverImageTween.OnComplete(() => hoverImage.gameObject.SetActive(false));
        }

        public override void OnPointerDown()
        {
            SetInteracted(true);
        }

        public override void OnPointerUp()
        {
            SetInteracted(false);
        }

        public void SetInteracted(bool interacted)
        {
            if (interacted)
            {
                _scaleTween?.Kill();
                _scaleTween = transform.DOScale(Vector2.one * 0.9f, AnimationDuration);
                
                OnPointerEnter();
            }
            else
            {
                _scaleTween?.Kill();
                _scaleTween = transform.DOScale(Vector2.one, AnimationDuration);
                
                OnPointerExit();
            }
        }

        protected override void ResetView()
        {
            _scaleTween?.Kill();
            _baseImageTween?.Kill();
            _hoverImageTween?.Kill();
            
        }
    }
}