using System.Collections.Generic;
using DG.Tweening;
using StairwayGamesTest.Data;
using StairwayGamesTest.UI.Input;
using TMPro;
using UnityEngine;

namespace StairwayGamesTest.UI
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private ButtonController openCraftingButton;
        [SerializeField] private ButtonController changeDisplayButton;
        [SerializeField] private ButtonController changeResolutionButton;
        [SerializeField] private ButtonController resetInventoryButton;
        [SerializeField] private ButtonController exitButton;

        [SerializeField] private CanvasGroup inventoryCanvas;
        [SerializeField] private InventoryCanvasController inventoryCanvasController;
        [SerializeField] private TextMeshProUGUI displayModeText;
        [SerializeField] private TextMeshProUGUI resolutionText;

        [SerializeField] private List<Vector2> resolutions;
        private int _currentResolutionIndex;
        private FullScreenMode _currentDisplayMode;

        private void Awake()
        {
            openCraftingButton.ButtonUp += OnOpenCraftingButtonPressed;
            changeDisplayButton.ButtonUp += OnChangeDisplayPressed;
            changeResolutionButton.ButtonUp += OnChangeResolutionButtonPressed;
            resetInventoryButton.ButtonUp += OnResetInventoryButtonPressed;
            exitButton.ButtonUp += OnExitButtonPressed;
            inventoryCanvasController.CanvasClosed += OnInventoryCanvasClosed;

            _currentDisplayMode = FullScreenMode.Windowed;
        }

        private void OnDestroy()
        {
            openCraftingButton.ButtonUp -= OnOpenCraftingButtonPressed;
            changeDisplayButton.ButtonUp -= OnChangeDisplayPressed;
            changeResolutionButton.ButtonUp -= OnChangeResolutionButtonPressed;
            resetInventoryButton.ButtonUp -= OnResetInventoryButtonPressed;
            exitButton.ButtonUp -= OnExitButtonPressed;
            inventoryCanvasController.CanvasClosed -= OnInventoryCanvasClosed;
        }

        private Tween _inventoryCanvasTween;
        private void OnOpenCraftingButtonPressed()
        {
            _inventoryCanvasTween?.Kill();
            inventoryCanvas.gameObject.SetActive(true);
            inventoryCanvas.interactable = true;
            _inventoryCanvasTween = inventoryCanvas.DOFade(1f, 0.5f);
            gameObject.SetActive(false);
        }

        private void OnInventoryCanvasClosed()
        {
            _inventoryCanvasTween?.Kill();
            inventoryCanvas.interactable = false;
            _inventoryCanvasTween = inventoryCanvas.DOFade(0f, 0.2f);
            _inventoryCanvasTween.OnComplete(() => inventoryCanvas.gameObject.SetActive(false));
            gameObject.SetActive(true);
        }

        private void OnChangeDisplayPressed()
        {
            switch (_currentDisplayMode)
            {
                case FullScreenMode.Windowed:
                    _currentDisplayMode = FullScreenMode.FullScreenWindow;
                    displayModeText.text = "Display Mode (Borderless)";
                    Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, _currentDisplayMode);
                    resolutionText.text = $"Resolution ({Screen.currentResolution.width} x {Screen.currentResolution.height})";
                    break;
                case FullScreenMode.FullScreenWindow:
                    _currentDisplayMode = FullScreenMode.Windowed;
                    displayModeText.text = "Display Mode (Windowed)";
                    var currentRes = resolutions[_currentResolutionIndex];
                    Screen.SetResolution((int)currentRes.x, (int)currentRes.y, _currentDisplayMode);
                    resolutionText.text = $"Resolution ({currentRes.x} x {currentRes.y})";
                    break;
            }
        }

        private void OnChangeResolutionButtonPressed()
        {
            if (_currentDisplayMode == FullScreenMode.FullScreenWindow) return;
            
            if (_currentResolutionIndex >= resolutions.Count - 1)
            {
                _currentResolutionIndex = 0;
            }
            else
            {
                _currentResolutionIndex++;
            }

            var selectedRes = resolutions[_currentResolutionIndex];
            Screen.SetResolution((int) selectedRes.x, (int) selectedRes.y, _currentDisplayMode);
            resolutionText.text = $"Resolution ({selectedRes.x} x {selectedRes.y})";
        }

        private void OnResetInventoryButtonPressed()
        {
            DataController.ResetInventory();
        }

        private void OnExitButtonPressed()
        {
            Application.Quit();
        }
    }
}