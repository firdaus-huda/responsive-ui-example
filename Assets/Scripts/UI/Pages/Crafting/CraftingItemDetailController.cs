using System;
using StairwayGamesTest.Data.Game;
using UnityEngine;

namespace StairwayGamesTest.UI.Pages.Crafting
{
    public class CraftingItemDetailController : MonoBehaviour
    {
        private CraftingItemDetailView _view;

        private void Awake()
        {
            _view = GetComponent<CraftingItemDetailView>();
        }

        public void SetData(ItemInfo itemInfo)
        {
            _view.SetData(itemInfo);
        }

        public void OnCraftingSuccess()
        {
            _view.RefreshData();
        }

        public void OnCraftingFailed()
        {
            _view.OnCraftingFailed();
        }
    }
}