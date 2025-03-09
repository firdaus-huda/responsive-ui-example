using StairwayGamesTest.UI.Input;

namespace StairwayGamesTest.UI
{
    public class TabButtonController : ButtonController
    {
        public void SetSelected(bool selected)
        {
            if (View is TabButtonView view)
            {
                view.SetSelected(selected);
            }
        }
    }
}