using System;
using DG.Tweening;
using StairwayGamesTest.UI.Input;
using UnityEngine;
using UnityEngine.EventSystems;

namespace StairwayGamesTest.UI
{
    public class TabNavButtonController : ButtonController
    {
        [SerializeField] private KeyCode inputKeycode;

        public event Action KeycodePressed;

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(inputKeycode))
            {
                OnKeycodeDown();
            }
            if (UnityEngine.Input.GetKeyUp(inputKeycode))
            {
                OnKeycodeUp();
            }
        }

        private void OnKeycodeDown()
        {
            if (View is TabNavButtonView view)
            {
                view.SetInteracted(true);
            }
        }

        private void OnKeycodeUp()
        {
            KeycodePressed?.Invoke();
            if (View is TabNavButtonView view)
            {
                view.SetInteracted(false);
            }
        }
    }
}