using System;
using DG.Tweening;
using StairwayGamesTest.Common;
using StairwayGamesTest.Data.Game;
using StairwayGamesTest.UI.Input;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace StairwayGamesTest.UI.Pages.Crafting
{
    public class CraftingListItemView : ButtonView
    {
        [SerializeField] private Image icon;
        [SerializeField] private Image placeholderIcon;
        [SerializeField] private Image hoverFrame;
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private CanvasGroup itemNamePopup;

        private const float AnimationDuration = 0.2f;

        private Tween _hoverTween;

        public void SetData(ItemInfo itemInfo)
        {
            itemName.text = itemInfo.itemName;
            var sprite = ResourceManager.GetSprite(itemInfo.spriteName);
            icon.sprite = sprite;
            if (sprite != null)
            {
                placeholderIcon.gameObject.SetActive(false);
                icon.gameObject.SetActive(true);
            }
        }

        public override void OnPointerEnter()
        {
            _hoverTween?.Kill();
            _hoverTween = hoverFrame.DOFade(1f, AnimationDuration);
            itemNamePopup.gameObject.SetActive(true);
            itemNamePopup.DOFade(1f, AnimationDuration);
            itemNamePopup.transform.localScale = Vector2.one * 0.9f;
            itemNamePopup.transform.DOScale(Vector2.one, AnimationDuration);
        }

        public override void OnPointerExit()
        {
            _hoverTween?.Kill();
            _hoverTween = hoverFrame.DOFade(0f, AnimationDuration);
            itemNamePopup.alpha = 0f;
            itemNamePopup.gameObject.SetActive(false);
        }

        protected override void ResetView()
        {
            _hoverTween?.Kill();
            itemNamePopup.alpha = 0f;
            itemNamePopup.gameObject.SetActive(false);
            hoverFrame.color = new Color(hoverFrame.color.r, hoverFrame.color.g, hoverFrame.color.b, 0f);
        }
    }
}