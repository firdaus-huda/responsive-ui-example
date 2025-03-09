using System;
using System.Collections.Generic;
using System.Linq;
using StairwayGamesTest.Data.Enums;
using StairwayGamesTest.Data.Game;
using UnityEngine;

namespace StairwayGamesTest.Data
{
    public static class DataController
    {
        private static readonly DataModel Model;

        private static List<ItemInfo> _itemInfo = new();

        public static event Action DataChanged;

        static DataController()
        {
            Model = new DataModel();
            _itemInfo = Resources.Load<ItemInfoScriptableObject>("ItemInfo").itemInfo;
        }

        public static ItemInfo[] GetAllItemInfo()
        {
            if (_itemInfo == null || _itemInfo.Count == 0)
            {
                _itemInfo = Resources.Load<ItemInfoScriptableObject>("ItemInfo").itemInfo;
            }

            return _itemInfo.ToArray();
        }

        public static ItemInfo GetItemInfo(this ItemId itemId)
        {
            if (itemId == ItemId.None) return null;
            
            return _itemInfo.Find(x => x.itemId == itemId);
        }

        public static int GetOwnedItemAmount(ItemId itemId)
        {
            int amount = 0;
            foreach (var item in Model.InventorySlots)
            {
                if (item.ItemId == itemId)
                {
                    amount += item.Amount;
                }
            }

            return amount;
        }

        public static bool CraftItem(ItemId itemId)
        {
            if (itemId == ItemId.None) return false;
            if (!CheckSlotAvailableForCrafting(itemId)) return false;
            
            Dictionary<ItemId, int> ownedItemDeduction = new();
            foreach (var recipe in GetItemInfo(itemId).craftingRecipe)
            {
                if (GetOwnedItemAmount(recipe.itemId) >= recipe.amount)
                {
                    ownedItemDeduction[recipe.itemId] = recipe.amount;
                    continue;
                }

                return false;
            }

            foreach (var item in ownedItemDeduction)
            {
                var amountToDeduct = item.Value;
                foreach (var slot in Model.InventorySlots)
                {
                    if (slot.ItemId == item.Key)
                    {
                        if (slot.Amount <= amountToDeduct)
                        {
                            amountToDeduct -= slot.Amount;
                            slot.ItemId = ItemId.None;
                            slot.Amount = 0;
                        }
                        else if (slot.Amount > amountToDeduct)
                        {
                            slot.Amount -= amountToDeduct;
                            break;
                        }
                    }
                }
            }

            AddItem(itemId, 1);
            
            return true;
        }

        public static bool CheckSlotAvailableForCrafting(ItemId itemId)
        {
            return Model.InventorySlots.Exists(x => x.ItemId == itemId) || Model.InventorySlots.Any(slot => slot.ItemId == ItemId.None && !slot.Locked);
        }

        public static void AddItem(ItemId itemId, int amount)
        {
            foreach (var slot in Model.InventorySlots)
            {
                if (slot.ItemId == itemId)
                {
                    slot.Amount += amount;
                    DataChanged?.Invoke();
                    return;
                }
            }

            foreach (var slot in Model.InventorySlots)
            {
                if (slot.ItemId == ItemId.None && !slot.Locked)
                {
                    slot.ItemId = itemId;
                    slot.Amount = amount;
                    DataChanged?.Invoke();
                    return;
                }
            }
        }

        public static List<DataModel.ItemSlot> GetInventorySlots()
        {
            return Model.InventorySlots;
        }

        private static void MoveInventoryItem()
        {
            
        }

        public static void SetInventoryLock(int slotIndex, bool locked)
        {
            Model.InventorySlots[slotIndex].Locked = locked;
        }

        public static int GetMaxInventorySlot()
        {
            return Model.MaxInventorySlot;
        }

        public static void ResetInventory() //Mock data
        {
            Model.InventorySlots = new();

            for (int i = 0; i < 40; i++)
            {
                Model.InventorySlots.Add(new DataModel.ItemSlot());
            }
            
            for (int i = 0; i < 7; i++)
            {
                SetInventoryLock(i, false);
            }
                
            AddItem(ItemId.Fiber, 400);
            AddItem(ItemId.Wood, 500);
            AddItem(ItemId.Stone, 300);
        }
    }
}