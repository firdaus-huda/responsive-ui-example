using StairwayGamesTest.UI.Input;
using UnityEngine;

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