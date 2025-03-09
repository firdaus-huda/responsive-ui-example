using System.Collections.Generic;
using StairwayGamesTest.Data;
using UnityEngine;

namespace StairwayGamesTest.UI.Pages.Crafting
{
    public class CraftingInventoryController : MonoBehaviour
    {
        [SerializeField] private CraftingInventoryListItemController listItemPrefab;
        [SerializeField] private GameObject listItemParent;
        
        private readonly List<CraftingInventoryListItemController> _listItems = new();

        private void Awake()
        {
            DataController.DataChanged += RefreshInventory;

            for (int i = 0; i < DataController.GetMaxInventorySlot(); i++)
            {
                var slot = Instantiate(listItemPrefab, listItemParent.transform);
                _listItems.Add(slot);
            }
            
            RefreshInventory();
        }

        private void OnDestroy()
        {
            DataController.DataChanged -= RefreshInventory;
        }

        private void RefreshInventory()
        {
            var inventorySlots = DataController.GetInventorySlots();

            for (int i = 0; i < inventorySlots.Count; i++)
            {
                var slot = inventorySlots[i];
                _listItems[i].SetData(slot.ItemId, slot.Amount, slot.Locked);
            }
        }
    }
}