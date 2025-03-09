using System;
using System.Collections.Generic;
using DG.Tweening;
using StairwayGamesTest.Common;
using StairwayGamesTest.Data;
using StairwayGamesTest.Data.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace StairwayGamesTest.UI.Pages.Crafting
{
    public class CraftingPageController : PageController
    {
        [SerializeField] private CraftingSidebarController craftingSidebarController;
        [SerializeField] private CraftingListItemController listItemPrefab;
        [SerializeField] private CraftingItemDetailController craftingItemDetailController;
        [SerializeField] private ErrorPopup errorPopup;
        [SerializeField] private Transform listItemParent;
        [SerializeField] private CraftingScrollRect scrollRect;

        private readonly List<CraftingListItemController> _craftingListItems = new();

        protected override void Awake()
        {
            base.Awake();
            var itemInfo = DataController.GetAllItemInfo();
            int totalItemCount = 160; //Placeholder
            
            foreach (var info in itemInfo)
            {
                if (!info.craftable) continue;
                var item = Instantiate(listItemPrefab, listItemParent);
                item.SetData(info);
                _craftingListItems.Add(item);
                totalItemCount--;
            }
            
            for (int i = 0; i < totalItemCount; i++) //Placeholder
            {
                var item = Instantiate(listItemPrefab, listItemParent);
                _craftingListItems.Add(item);
            }

            foreach (var item in _craftingListItems)
            {
                item.CraftingButtonPressed += OnCraftingListItemPressed;
                item.CraftingButtonHovered += OnCraftingListItemHovered;
            }

            craftingSidebarController.CraftingTypeSelected += OnCraftingTypeSelected;
        }

        private void Start()
        {
            craftingItemDetailController.SetData(_craftingListItems[0].ItemId.GetItemInfo());
        }

        private void OnDestroy()
        {
            foreach (var item in _craftingListItems)
            {
                item.CraftingButtonPressed -= OnCraftingListItemPressed;
                item.CraftingButtonHovered -= OnCraftingListItemHovered;
            }
            
            craftingSidebarController.CraftingTypeSelected -= OnCraftingTypeSelected;
        }

        private void OnEnable()
        {
            scrollRect.ResetPosition();
        }

        private void OnCraftingListItemHovered(ItemId itemId)
        {
            craftingItemDetailController.SetData(itemId.GetItemInfo());
        }

        private void OnCraftingListItemPressed(ItemId itemId)
        {
            if (itemId == ItemId.None) return;

            if (!DataController.CheckSlotAvailableForCrafting(itemId))
            {
                errorPopup.ShowError("Your inventory is full.");
                return;
            }

            if (!DataController.CraftItem(itemId))
            {
                craftingItemDetailController.OnCraftingFailed();
                errorPopup.ShowError("You have insufficient material for this recipe.");
                return;
            }
            
            AudioEngine.PlaySfx("switch10");
            craftingItemDetailController.OnCraftingSuccess();
        }
        
        private void OnCraftingTypeSelected(ItemType filter)
        {
            if (View is CraftingPageView view) view.OnFilterChanged();
            
            switch (filter)
            {
                case ItemType.None:
                    foreach (var item in _craftingListItems)
                    {
                        item.gameObject.SetActive(true);
                    }
                    break;
                case ItemType.Farming:
                    foreach (var item in _craftingListItems)
                    {
                        if (item.ItemType == ItemType.Farming || item.ItemType == ItemType.None) item.gameObject.SetActive(true);
                        else item.gameObject.SetActive(false);
                    }
                    break;
                case ItemType.Artisan:
                    foreach (var item in _craftingListItems)
                    {
                        if (item.ItemType == ItemType.Artisan || item.ItemType == ItemType.None) item.gameObject.SetActive(true);
                        else item.gameObject.SetActive(false);
                    }
                    break;
                case ItemType.Decor:
                    foreach (var item in _craftingListItems)
                    {
                        if (item.ItemType == ItemType.Decor || item.ItemType == ItemType.None) item.gameObject.SetActive(true);
                        else item.gameObject.SetActive(false);
                    }
                    break;
                case ItemType.Scarecrow:
                    foreach (var item in _craftingListItems)
                    {
                        if (item.ItemType == ItemType.Scarecrow || item.ItemType == ItemType.None) item.gameObject.SetActive(true);
                        else item.gameObject.SetActive(false);
                    }
                    break;
                case ItemType.Misc:
                    foreach (var item in _craftingListItems)
                    {
                        if (item.ItemType == ItemType.Misc || item.ItemType == ItemType.None) item.gameObject.SetActive(true);
                        else item.gameObject.SetActive(false);
                    }
                    break;
            }
        }
    }
}