using System;
using System.Collections.Generic;
using DG.Tweening;
using StairwayGamesTest.Common;
using StairwayGamesTest.Data;
using StairwayGamesTest.Data.Enums;
using StairwayGamesTest.Data.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StairwayGamesTest.UI.Pages.Crafting
{
    public class CraftingItemDetailView : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private TextMeshProUGUI itemType;
        [SerializeField] private TextMeshProUGUI itemDescription;
        [SerializeField] private Image recipeContainer;
        [SerializeField] private List<CraftingItemDetailRecipeView> recipeItems = new();

        private RectTransform _rectTransform;

        private const float AnimationDuration = 0.2f;
        
        private Tween _recipeContainerTween;
        private Tween _rectTransformTween;

        private ItemInfo _itemInfo;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void SetData(ItemInfo itemInfo)
        {
            if (itemInfo == null || itemInfo.itemType == ItemType.None) return;

            _itemInfo = itemInfo;
            RefreshData();
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);
            
            _rectTransformTween?.Kill();
            _rectTransform.localScale = Vector2.one * 0.95f;
            _rectTransformTween = _rectTransform.DOScale(Vector2.one, AnimationDuration);
            
            _recipeContainerTween?.Kill();
            _recipeContainerTween = recipeContainer.DOFade(0f, 0f);
        }

        public void RefreshData()
        {
            
            icon.sprite = ResourceManager.GetSprite(_itemInfo.spriteName);
            itemName.text = _itemInfo.itemName;
            itemType.text = _itemInfo.itemType.ToString();
            itemDescription.text = _itemInfo.description;

            foreach (var item in recipeItems)
            {
                item.gameObject.SetActive(false);
            }

            for (int i = 0; i < _itemInfo.craftingRecipe.Count; i++)
            {
                var recipe = _itemInfo.craftingRecipe[i];
                recipeItems[i].gameObject.SetActive(true);
                recipeItems[i].SetData(recipe.itemId, recipe.amount, DataController.GetOwnedItemAmount(recipe.itemId));
            }
        }

        public void OnCraftingFailed()
        {
            _recipeContainerTween.Kill();
            _recipeContainerTween = recipeContainer.DOFade(1f, AnimationDuration);
            _recipeContainerTween.OnComplete(() => _recipeContainerTween = recipeContainer.DOFade(0f, AnimationDuration).OnComplete(() => _recipeContainerTween = recipeContainer.DOFade(1f, AnimationDuration).OnComplete(() => _recipeContainerTween = recipeContainer.DOFade(0f, AnimationDuration))));
        }
    }
}