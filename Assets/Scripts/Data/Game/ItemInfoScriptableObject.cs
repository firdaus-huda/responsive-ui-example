using System;
using System.Collections.Generic;
using StairwayGamesTest.Data.Enums;
using UnityEngine;

namespace StairwayGamesTest.Data.Game
{
    [CreateAssetMenu(menuName = "Create/ItemInfo")]
    public class ItemInfoScriptableObject : ScriptableObject
    {
        public List<ItemInfo> itemInfo = new();
    }
}