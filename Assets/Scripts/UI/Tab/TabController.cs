using System;
using System.Collections.Generic;
using UnityEngine;

namespace StairwayGamesTest.UI
{
    public class TabController : MonoBehaviour
    {
        [SerializeField] private TabNavButtonController previousButton;
        [SerializeField] private TabNavButtonController nextButton;
        
        [SerializeField] private TabButtonController journalButton;
        [SerializeField] private TabButtonController questButton;
        [SerializeField] private TabButtonController craftingButton;
        [SerializeField] private TabButtonController inventoryButton;
        [SerializeField] private TabButtonController mapButton;
        [SerializeField] private TabButtonController relationshipButton;
        [SerializeField] private TabButtonController masteryButton;
        
        [SerializeField] private List<PageController> pages = new();

        private readonly List<TabButtonController> _tabButtonControllers = new();
        private TabButtonController _currentSelectedButton;
        private int _currentIndex = -1;

        private void Awake()
        {
            journalButton.ButtonUp += OnJournalButtonPressed;
            questButton.ButtonUp += OnQuestButtonPressed;
            craftingButton.ButtonUp += OnCraftingButtonPressed;
            inventoryButton.ButtonUp += OnInventoryButtonPressed;
            mapButton.ButtonUp += OnMapButtonPressed;
            relationshipButton.ButtonUp += OnRelationshipButtonPressed;
            masteryButton.ButtonUp += OnMasteryButtonPressed;

            previousButton.KeycodePressed += MovePrevious;
            previousButton.ButtonUp += MovePrevious;
            nextButton.KeycodePressed += MoveNext;
            nextButton.ButtonUp += MoveNext;
            
            _tabButtonControllers.Add(journalButton);
            _tabButtonControllers.Add(questButton);
            _tabButtonControllers.Add(craftingButton);
            _tabButtonControllers.Add(inventoryButton);
            _tabButtonControllers.Add(mapButton);
            _tabButtonControllers.Add(relationshipButton);
            _tabButtonControllers.Add(masteryButton);

            _currentIndex = -1;
        }

        private void OnDisable()
        {
            OnJournalButtonPressed();
        }

        private void OnDestroy()
        {
            journalButton.ButtonUp -= OnJournalButtonPressed;
            questButton.ButtonUp -= OnQuestButtonPressed;
            craftingButton.ButtonUp -= OnCraftingButtonPressed;
            inventoryButton.ButtonUp -= OnInventoryButtonPressed;
            mapButton.ButtonUp -= OnMapButtonPressed;
            relationshipButton.ButtonUp -= OnRelationshipButtonPressed;
            masteryButton.ButtonUp -= OnMasteryButtonPressed;
            
            previousButton.KeycodePressed -= MovePrevious;
            previousButton.ButtonUp -= MovePrevious;
            nextButton.KeycodePressed -= MoveNext;
            nextButton.ButtonUp -= MoveNext;
        }

        private void MovePrevious()
        {
            if (_currentIndex <= 0)
            {
                _currentIndex = _tabButtonControllers.Count - 1;
            }
            else
            {
                _currentIndex--;
            }

            _currentSelectedButton = _tabButtonControllers[_currentIndex];
            UpdateSelection();
        }

        private void MoveNext()
        {
            if (_currentIndex >= _tabButtonControllers.Count - 1)
            {
                _currentIndex = 0;
            }
            else
            {
                _currentIndex++;
            }

            _currentSelectedButton = _tabButtonControllers[_currentIndex];
            UpdateSelection();
        }

        public void OpenCraftingPage()
        {
            OnCraftingButtonPressed();
        }

        private void OnJournalButtonPressed()
        {
            if (_currentSelectedButton == journalButton) return;
            
            _currentSelectedButton = journalButton;
            UpdateSelection();
        }
        private void OnQuestButtonPressed()
        {
            if (_currentSelectedButton == questButton) return;

            _currentSelectedButton = questButton;
            UpdateSelection();
        }
        private void OnCraftingButtonPressed()
        {
            if (_currentSelectedButton == craftingButton) return;

            _currentSelectedButton = craftingButton;
            UpdateSelection();
        }
        private void OnInventoryButtonPressed()
        {
            if (_currentSelectedButton == inventoryButton) return;

            _currentSelectedButton = inventoryButton;
            UpdateSelection();
        }
        private void OnMapButtonPressed()
        {
            if (_currentSelectedButton == mapButton) return;

            _currentSelectedButton = mapButton;
            UpdateSelection();
        }
        private void OnRelationshipButtonPressed()
        {
            if (_currentSelectedButton == relationshipButton) return;

            _currentSelectedButton = relationshipButton;
            UpdateSelection();
        }
        private void OnMasteryButtonPressed()
        {
            if (_currentSelectedButton == masteryButton) return;

            _currentSelectedButton = masteryButton;
            UpdateSelection();
        }

        private void UpdateSelection()
        {
            var pageIndex = _tabButtonControllers.IndexOf(_currentSelectedButton);
            
            foreach (var button in _tabButtonControllers)
            {
                button.SetSelected(false);
            }

            foreach (var page in pages)
            {
                page.SetEnabled(false);
            }
            
            _currentSelectedButton.SetSelected(true);
            _currentIndex = pageIndex;

            pages[pageIndex].SetEnabled(true);
        }
    }
}
