using System.Collections.Generic;
using StairwayGamesTest.Data.Enums;

namespace StairwayGamesTest.Data
{
    public class DataModel
    {
        public readonly int MaxInventorySlot = 40;

        public List<ItemSlot> InventorySlots;

        public DataModel()
        {
            InventorySlots = new();

            for (int i = 0; i < 40; i++)
            {
                InventorySlots.Add(new ItemSlot());
            }
        }
        
        public class ItemSlot
        {
            public ItemId ItemId = ItemId.None;
            public int Amount = 0;
            public bool Locked = true;
        }
    }
}