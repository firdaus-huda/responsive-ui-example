using StairwayGamesTest.Data.Enums;
using StairwayGamesTest.Data.Game;
using StairwayGamesTest.UI.Input;

namespace StairwayGamesTest.UI.Pages.Crafting
{
    public class CraftingInventoryListItemController : ButtonController
    {
        public void SetData(ItemId itemId, int amount, bool locked)
        {
            if (View is CraftingInventoryListItemView view)
            {
                view.SetData(itemId, amount, locked);
            }
        }
    }
}