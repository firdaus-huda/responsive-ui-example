using StairwayGamesTest.Common;
using StairwayGamesTest.Data;
using StairwayGamesTest.Data.Enums;
using StairwayGamesTest.Data.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StairwayGamesTest.UI.Pages.Crafting
{
    public class CraftingItemDetailRecipeView : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private TextMeshProUGUI requirement;

        public void SetData(ItemId itemId, int requiredResource, int availableResource)
        {
            var itemInfo = itemId.GetItemInfo();
            icon.sprite = ResourceManager.GetSprite(itemInfo.spriteName);
            itemName.text = itemInfo.itemName;
            requirement.text = $"{availableResource}/{requiredResource}";

            if (availableResource >= requiredResource)
            {
                itemName.color = Color.black;
                requirement.color = Color.black;
            }
            else
            {
                itemName.color = new Color32(234, 78, 78, 255);
                requirement.color = new Color32(234, 78, 78, 255);
            }
        }
    }
}