using DG.Tweening;
using StairwayGamesTest.Common;
using StairwayGamesTest.Data;
using StairwayGamesTest.Data.Enums;
using StairwayGamesTest.UI.Input;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StairwayGamesTest.UI.Pages.Crafting
{
    public class CraftingInventoryListItemView : ButtonView
    {
        [SerializeField] private Image hoverFrame;
        [SerializeField] private CanvasGroup infoPopup;
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private Image icon;
        [SerializeField] private GameObject normalFrame;
        [SerializeField] private GameObject lockedFrame;
        [SerializeField] private GameObject amountFrame;
        [SerializeField] private TextMeshProUGUI amountText;

        private const float AnimationDuration = 0.2f;

        private Tween _hoverFrameTween;
        private Tween _infoFadeTween;
        private Tween _infoScaleTween;

        private ItemId _itemId;
        private bool _locked;

        public void SetData(ItemId itemId, int amount, bool locked)
        {
            _itemId = itemId;
            _locked = locked;
            
            if (_locked)
            {
                lockedFrame.SetActive(true);
                normalFrame.SetActive(false);
                icon.gameObject.SetActive(false);
                hoverFrame.gameObject.SetActive(false);
                amountFrame.SetActive(false);
                return;
            }

            if (itemId != ItemId.None)
            {
                var info = itemId.GetItemInfo();
                itemName.text = info.itemName;
                icon.sprite = ResourceManager.GetSprite(info.spriteName);
            }
            
            amountFrame.SetActive(amount > 1);
            normalFrame.SetActive(true);
            lockedFrame.SetActive(false);
            hoverFrame.gameObject.SetActive(true);
            amountText.text = amount.ToString();
            icon.gameObject.SetActive(itemId != ItemId.None);
            LayoutRebuilder.ForceRebuildLayoutImmediate(infoPopup.GetComponent<RectTransform>());
        }
        public override void OnPointerEnter()
        {
            if (_locked || _itemId == ItemId.None) return;
            
            _infoFadeTween?.Kill();
            _infoScaleTween?.Kill();
            _hoverFrameTween?.Kill();
            _hoverFrameTween = hoverFrame.DOFade(1f, AnimationDuration);

            infoPopup.transform.localScale = Vector2.one * 0.9f;
            infoPopup.alpha = 0f;

            _infoScaleTween = infoPopup.transform.DOScale(1f, AnimationDuration);
            _infoFadeTween = infoPopup.DOFade(1f, AnimationDuration);
        }

        public override void OnPointerExit()
        {
            if (_locked || _itemId == ItemId.None) return;
            
            _hoverFrameTween?.Kill();
            _hoverFrameTween = hoverFrame.DOFade(0f, AnimationDuration);

            _infoFadeTween?.Kill();
            infoPopup.alpha = 0f;
        }

        protected override void ResetView()
        {
            _infoFadeTween?.Kill();
            _infoScaleTween?.Kill();
            _hoverFrameTween?.Kill();

            hoverFrame.color = new Color(hoverFrame.color.r, hoverFrame.color.g, hoverFrame.color.b, 0f);
            infoPopup.alpha = 0f;
        }
    }
}