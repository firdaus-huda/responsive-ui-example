using System;
using System.Collections.Generic;
using StairwayGamesTest.Data.Enums;
using UnityEngine;

namespace StairwayGamesTest.UI.Pages.Crafting
{
    public class CraftingSidebarController : MonoBehaviour
    {
        [SerializeField] private CraftingSidebarButtonController allButton;
        [SerializeField] private CraftingSidebarButtonController farmingButton;
        [SerializeField] private CraftingSidebarButtonController artisanButton;
        [SerializeField] private CraftingSidebarButtonController decorButton;
        [SerializeField] private CraftingSidebarButtonController scarecrowButton;
        [SerializeField] private CraftingSidebarButtonController miscButton;

        public event Action<ItemType> CraftingTypeSelected;
        
        private CraftingSidebarButtonController _currentSelectedButton;
        private readonly List<CraftingSidebarButtonController> _craftingSidebarButtonControllers = new();

        private void Awake()
        {
            allButton.ButtonUp += OnAllButtonPressed;
            farmingButton.ButtonUp += OnFarmingButtonPressed;
            artisanButton.ButtonUp += OnArtisanButtonPressed;
            decorButton.ButtonUp += OnDecorButtonPressed;
            scarecrowButton.ButtonUp += OnScarecrowButtonPressed;
            miscButton.ButtonUp += OnMiscButtonPressed;
            
            _craftingSidebarButtonControllers.Add(allButton);
            _craftingSidebarButtonControllers.Add(farmingButton);
            _craftingSidebarButtonControllers.Add(artisanButton);
            _craftingSidebarButtonControllers.Add(decorButton);
            _craftingSidebarButtonControllers.Add(scarecrowButton);
            _craftingSidebarButtonControllers.Add(miscButton);
        }

        private void Start()
        {
            OnAllButtonPressed();
        }

        private void OnDestroy()
        {
            allButton.ButtonUp -= OnAllButtonPressed;
            farmingButton.ButtonUp -= OnFarmingButtonPressed;
            artisanButton.ButtonUp -= OnArtisanButtonPressed;
            decorButton.ButtonUp -= OnDecorButtonPressed;
            scarecrowButton.ButtonUp -= OnScarecrowButtonPressed;
            miscButton.ButtonUp -= OnMiscButtonPressed;
        }

        private void OnAllButtonPressed()
        {
            _currentSelectedButton = allButton;
            UpdateSelection();
            CraftingTypeSelected?.Invoke(ItemType.None);
        }
        
        private void OnFarmingButtonPressed()
        {
            _currentSelectedButton = farmingButton;
            UpdateSelection();
            CraftingTypeSelected?.Invoke(ItemType.Farming);
        }
        
        private void OnArtisanButtonPressed()
        {
            _currentSelectedButton = artisanButton;
            UpdateSelection();
            CraftingTypeSelected?.Invoke(ItemType.Artisan);
        }
        
        private void OnDecorButtonPressed()
        {
            _currentSelectedButton = decorButton;
            UpdateSelection();
            CraftingTypeSelected?.Invoke(ItemType.Decor);
        }
        
        private void OnScarecrowButtonPressed()
        {
            _currentSelectedButton = scarecrowButton;
            UpdateSelection();
            CraftingTypeSelected?.Invoke(ItemType.Scarecrow);
        }
        
        private void OnMiscButtonPressed()
        {
            _currentSelectedButton = miscButton;
            UpdateSelection();
            CraftingTypeSelected?.Invoke(ItemType.Misc);
        }

        private void UpdateSelection()
        {
            foreach (var button in _craftingSidebarButtonControllers)
            {
                button.SetSelected(false);
            }
            _currentSelectedButton.SetSelected(true);
        }
    }
}