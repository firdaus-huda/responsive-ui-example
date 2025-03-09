using System;
using StairwayGamesTest.Data.Enums;
using StairwayGamesTest.Data.Game;
using StairwayGamesTest.UI.Input;
using UnityEngine.EventSystems;

namespace StairwayGamesTest.UI.Pages.Crafting
{
    public class CraftingListItemController : ButtonController
    {
        public event Action<ItemId> CraftingButtonPressed;
        public event Action<ItemId> CraftingButtonHovered;
        public ItemId ItemId { get; private set; }
        public ItemType ItemType { get; private set; }
        public void SetData(ItemInfo itemInfo)
        {
            ItemId = itemInfo.itemId;
            ItemType = itemInfo.itemType;

            if (View is CraftingListItemView view)
            {
                view.SetData(itemInfo);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            CraftingButtonPressed = null;
            CraftingButtonHovered = null;
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            CraftingButtonHovered?.Invoke(ItemId);
            if (ItemId == ItemId.None) return;
            base.OnPointerEnter(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            CraftingButtonPressed?.Invoke(ItemId);
            if (ItemId == ItemId.None) return;
            base.OnPointerUp(eventData);
        }
    }
}