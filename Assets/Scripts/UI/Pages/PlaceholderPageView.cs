using System;
using DG.Tweening;
using UnityEngine;

namespace StairwayGamesTest.UI
{
    public class PlaceholderPageView: PageView
    {
        [SerializeField] private GameObject pageContent;
        [SerializeField] private CanvasGroup canvasGroup;

        private Tween _pageContentTween;
        private Tween _canvasGroupTween;

        private void Awake()
        {
            canvasGroup.alpha = 0f;
        }

        protected override void TransitionIn()
        {
            _canvasGroupTween?.Kill();
            _canvasGroupTween = canvasGroup.DOFade(1f, AnimationDuration);
        }

        protected override void TransitionOut()
        {
            _canvasGroupTween?.Kill();
            _canvasGroupTween = canvasGroup.DOFade(0f, AnimationDuration);
        }
    }
}