using UnityEngine;

namespace StairwayGamesTest.UI
{
    public abstract class PageView: MonoBehaviour
    {
        private bool _isEnabled;
        
        protected const float AnimationDuration = 0.2f;
        public void SetEnabled(bool enable)
        {
            if (enable)
            {
                TransitionIn();
            }
            else
            {
                if (_isEnabled)
                {
                    TransitionOut();
                }
            }

            _isEnabled = enable;
        }

        protected virtual void TransitionIn() {}
        
        protected virtual void TransitionOut() {}
    }
}