using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace StairwayGamesTest.UI.Input
{
    public abstract class ButtonView : MonoBehaviour
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