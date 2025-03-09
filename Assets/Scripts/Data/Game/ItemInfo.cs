using System;
using System.Collections.Generic;
using StairwayGamesTest.Data.Enums;
using UnityEngine;

namespace StairwayGamesTest.Data.Game
{
    [Serializable]
    public class ItemInfo
    {
        public ItemId itemId;
        public ItemType itemType;
        public string itemName;
        [TextArea(1, 3)] public string description;
        public string spriteName;
        public bool craftable;
        public List<CraftingRecipe> craftingRecipe = new();
    }

    [Serializable]
    public class CraftingRecipe
    {
        public ItemId itemId;
        public int amount;
    }
}