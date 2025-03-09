using System.Collections.Generic;
using UnityEngine;

namespace StairwayGamesTest.Common
{
    public static class ResourceManager
    {
        private static SpriteDataScriptableObject _spriteData;

        private static readonly Dictionary<string, Sprite> SpriteCache = new();

        public static Sprite GetSprite(string spriteName)
        {
            if (string.IsNullOrEmpty(spriteName)) return null;
            if (_spriteData == null) _spriteData = Resources.Load<SpriteDataScriptableObject>("SpriteData");
            SpriteCache.TryAdd(spriteName, _spriteData.GetSprite(spriteName));

            return SpriteCache[spriteName];
        }
    }
}