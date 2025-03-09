using StairwayGamesTest.UI.Input;

namespace StairwayGamesTest.UI.Pages.Crafting
{
    public class CraftingSidebarButtonController : ButtonController
    {
        public void SetSelected(bool selected)
        {
            if (View is CraftingSidebarButtonView view)
            {
                view.SetSelected(selected);
            }
        }
    }
}