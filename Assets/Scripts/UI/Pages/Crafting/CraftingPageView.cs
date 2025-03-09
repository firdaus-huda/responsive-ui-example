using DG.Tweening;
using UnityEngine;

namespace StairwayGamesTest.UI.Pages.Crafting
{
    public class CraftingPageView : PageView
    {
        [SerializeField] private RectTransform pageContent;
        [SerializeField] private RectTransform sidebar;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private CanvasGroup listItemCanvasGroup;

        private Tween _pageContentTween;
        private Tween _canvasGroupTween;
        private Tween _sidebarTween;
        private Tween _listItemCanvasGroupTween;
        private float _pageContentInitialRectXPosition;
        private float _sidebarInitialRectXPosition;
        private const float RectXPositionDelta = 10f;

        private void Awake()
        {
            _pageContentInitialRectXPosition = pageContent.anchoredPosition.x;
            _sidebarInitialRectXPosition = sidebar.anchoredPosition.x;
            canvasGroup.alpha = 0f;
        }

        protected override void TransitionIn()
        {
            _pageContentTween?.Kill();
            pageContent.anchoredPosition = new Vector2(pageContent.anchoredPosition.x + RectXPositionDelta, pageContent.anchoredPosition.y);
            _pageContentTween = pageContent.DOAnchorPosX(_pageContentInitialRectXPosition, AnimationDuration);
            
            _sidebarTween?.Kill();
            sidebar.anchoredPosition = new Vector2(sidebar.anchoredPosition.x - RectXPositionDelta, sidebar.anchoredPosition.y);
            _sidebarTween = sidebar.DOAnchorPosX(_sidebarInitialRectXPosition, AnimationDuration);
            
            _canvasGroupTween?.Kill();
            _canvasGroupTween = canvasGroup.DOFade(1f, AnimationDuration);
        }

        protected override void TransitionOut()
        {
            _canvasGroupTween?.Kill();
            _canvasGroupTween = canvasGroup.DOFade(0f, AnimationDuration);
            
            _pageContentTween?.Kill();
            pageContent.anchoredPosition = new Vector2(_pageContentInitialRectXPosition, pageContent.anchoredPosition.y);
            _pageContentTween = pageContent.DOAnchorPosX(pageContent.anchoredPosition.x + RectXPositionDelta, AnimationDuration);
            
            _sidebarTween?.Kill();
            sidebar.anchoredPosition = new Vector2(_sidebarInitialRectXPosition, sidebar.anchoredPosition.y);
            _sidebarTween = sidebar.DOAnchorPosX(sidebar.anchoredPosition.x - RectXPositionDelta, AnimationDuration);
        }

        public void OnFilterChanged()
        {
            _listItemCanvasGroupTween.Kill();
            listItemCanvasGroup.alpha = 0f;
            listItemCanvasGroup.DOFade(1f, AnimationDuration);
        }
    }
}