using System;
using DG.Tweening;
using StairwayGamesTest.Common;
using StairwayGamesTest.UI.Input;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StairwayGamesTest.UI
{
    public class TabButtonView : ButtonView
    {
        [SerializeField] private Image icon;
        [SerializeField] private RectTransform iconRect;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private CanvasGroup titlePopup;
        [SerializeField] private Image hoverFrame;

        [SerializeField] private Color normalColor;
        [SerializeField] private Color selectedColor;

        private const float AnimationDuration = 0.2f;
        
        private bool _isSelected;
        private Tween _hoverFrameTween;
        private Tween _titlePopupFadeTween;
        private Tween _titlePopupTransformTween;
        private float _initialIconYAnchoredPosition;
        private const float SelectedDeltaYAnchoredPosition = 10f;

        private void Awake()
        {
            _initialIconYAnchoredPosition = iconRect.anchoredPosition.y;
        }

        public override void OnPointerEnter()
        {
            if (_isSelected) return;
            _hoverFrameTween?.Kill();
            _hoverFrameTween = hoverFrame.DOFade(0.4f, AnimationDuration);
            
            titlePopup.gameObject.SetActive(true);
            
            _titlePopupFadeTween?.Kill();
            _titlePopupFadeTween = titlePopup.DOFade(1f, AnimationDuration);
            
            _titlePopupTransformTween?.Kill();
            titlePopup.transform.localScale = Vector2.one * 0.9f;
            _titlePopupTransformTween = titlePopup.transform.DOScale(Vector2.one, AnimationDuration);
        }

        public override void OnPointerExit()
        {
            if (_isSelected) return;
            _hoverFrameTween?.Kill();
            _hoverFrameTween = hoverFrame.DOFade(0f, 0f);
            
            _titlePopupFadeTween?.Kill();
            _titlePopupFadeTween = titlePopup.DOFade(0f, 0f);
            
            _titlePopupTransformTween?.Kill();
            titlePopup.transform.localScale = new Vector2(1f, 1f);
            _titlePopupTransformTween = titlePopup.transform.DOScale(Vector2.one * 0.9f, AnimationDuration);
            _titlePopupTransformTween.OnComplete(() => { titlePopup.gameObject.SetActive(false); });
        }

        public void SetSelected(bool selected)
        {
            if (selected)
            {
                icon.DOColor(selectedColor, AnimationDuration);
                iconRect.DOAnchorPosY(_initialIconYAnchoredPosition + SelectedDeltaYAnchoredPosition, AnimationDuration);
                title.DOFade(1f, AnimationDuration);
                _hoverFrameTween?.Kill();
                _hoverFrameTween = hoverFrame.DOFade(0f, 0f);
                
                _titlePopupFadeTween?.Kill();
                _titlePopupFadeTween = titlePopup.DOFade(0f, 0f);
            
                _titlePopupTransformTween?.Kill();
                titlePopup.transform.localScale = new Vector2(1f, 1f);
                _titlePopupTransformTween = titlePopup.transform.DOScale(Vector2.one * 0.9f, AnimationDuration);
                _titlePopupTransformTween.OnComplete(() => { titlePopup.gameObject.SetActive(false); });
                
                AudioEngine.PlaySfx("switch35");
            }
            else
            {
                if (_isSelected)
                {
                    iconRect.DOAnchorPosY(_initialIconYAnchoredPosition, AnimationDuration);
                    icon.DOColor(normalColor, AnimationDuration);
                    title.DOFade(0f, AnimationDuration);

                    _titlePopupFadeTween?.Kill();
                    _titlePopupFadeTween = titlePopup.DOFade(0f, 0f);
            
                    _titlePopupTransformTween?.Kill();
                    titlePopup.transform.localScale = new Vector2(1f, 1f);
                    _titlePopupTransformTween = titlePopup.transform.DOScale(Vector2.one * 0.9f, AnimationDuration);
                    _titlePopupTransformTween.OnComplete(() => { titlePopup.gameObject.SetActive(false); });
                }
            }

            _isSelected = selected;
        }

        protected override void ResetView()
        {
            _hoverFrameTween?.Kill();
            _titlePopupFadeTween?.Kill();
            _titlePopupTransformTween?.Kill();
            
            hoverFrame.color = new Color(hoverFrame.color.r, hoverFrame.color.g, hoverFrame.color.b, 0f);
            titlePopup.alpha = 0f;
        }
    }
}