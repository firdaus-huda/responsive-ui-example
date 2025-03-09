using System;
using UnityEngine;

namespace StairwayGamesTest.UI
{
    public class InventoryCanvasController : MonoBehaviour
    {
        [SerializeField] private TabController tabController;

        public event Action CanvasClosed;
        private async void OnEnable()
        {
            await new WaitForSeconds(0.1f);
            tabController.OpenCraftingPage();
        }

        private void OnDestroy()
        {
            CanvasClosed = null;
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyUp(KeyCode.Escape)) CanvasClosed?.Invoke();
        }
    }
}