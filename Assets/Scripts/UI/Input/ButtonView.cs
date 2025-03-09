using UnityEngine;

namespace StairwayGamesTest.UI.Input
{
    public class ButtonView : MonoBehaviour
    {
        private void OnDisable()
        {
            ResetView();
        }

        protected virtual void ResetView(){}
        public virtual void OnPointerEnter(){}
        public virtual void OnPointerExit(){}
        public virtual void OnPointerDown(){}
        public virtual void OnPointerUp(){}
    }
}