using System;
using UnityEngine;

namespace StairwayGamesTest.UI
{
    public class PageController: MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        protected PageView View;

        protected virtual void Awake()
        {
            View = GetComponent<PageView>();
        }

        public void SetEnabled(bool enable)
        {
            canvasGroup.interactable = enable;
            View.SetEnabled(enable);
        }
    }
}