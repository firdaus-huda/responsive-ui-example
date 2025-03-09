using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace StairwayGamesTest.UI.Input
{
    public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        protected ButtonView View;
        
        public event Action ButtonUp;
        public event Action ButtonDown;
        public event Action ButtonEnter;
        public event Action ButtonExit;

        private void Awake()
        {
            View = GetComponent<ButtonView>();
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (View != null) { View.OnPointerEnter(); }
            ButtonEnter?.Invoke();
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (View != null) { View.OnPointerExit(); }
            ButtonExit?.Invoke();
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (View != null) { View.OnPointerDown(); }
            ButtonDown?.Invoke();
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (View != null) { View.OnPointerUp(); }
            ButtonUp?.Invoke();
        }

        protected virtual void OnDestroy()
        {
            ButtonUp = null;
            ButtonDown = null;
            ButtonEnter = null;
            ButtonExit = null;
        }
    }
}