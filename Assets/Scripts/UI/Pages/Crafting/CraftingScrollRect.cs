using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace StairwayGamesTest.UI.Pages.Crafting
{
    public class CraftingScrollRect : ScrollRect
    {
        private GridLayoutGroup _layoutGroup;

        private const float ScrollDuration = 0.2f;

        private List<float> _snapPositions = new();
        private int _snapIndex;

        protected override void Awake()
        {
            base.Awake();
            _layoutGroup = content.GetComponent<GridLayoutGroup>();
        }

        protected override void Start()
        {
            base.Start();
            Initialize();
        }

        public void ResetPosition()
        {
            verticalNormalizedPosition = 1f;
            _snapIndex = 0;
        }

        private async void Initialize()
        {
            _snapPositions.Clear();
            await new WaitForSeconds(0.01f);
            float initialLayoutHeight = _layoutGroup.GetComponent<RectTransform>().rect.height;
            float layoutHeight = _layoutGroup.GetComponent<RectTransform>().rect.height;
            _snapPositions.Add(layoutHeight/initialLayoutHeight);
            while (layoutHeight > 0)
            {
                layoutHeight -= _layoutGroup.cellSize.y;
                layoutHeight -= _layoutGroup.spacing.y;
                layoutHeight -= _layoutGroup.spacing.y;
                layoutHeight -= _layoutGroup.spacing.y;
                float snap = layoutHeight / initialLayoutHeight;
                _snapPositions.Add(snap > 0? snap : 0);
            }
        }

        public override void OnBeginDrag(PointerEventData eventData) { }
        public override void OnDrag(PointerEventData eventData) { }
        public override void OnEndDrag(PointerEventData eventData) { }

        public override void OnScroll(PointerEventData data)
        {
            if (data.scrollDelta.y > 0)
            {
                if (verticalNormalizedPosition < 1)
                {
                    if (_snapIndex > 0)
                    {
                        _snapIndex--;
                        this.DOVerticalNormalizedPos(_snapPositions[_snapIndex], ScrollDuration);
                    }
                }
            }
            else
            {
                if (verticalNormalizedPosition > 0)
                {
                    if (_snapIndex < _snapPositions.Count - 1)
                    {
                        _snapIndex++;
                        this.DOVerticalNormalizedPos(_snapPositions[_snapIndex], ScrollDuration).SetEase(Ease.OutSine);
                    }
                }
            } 
        }
    }
}