using StairwayGamesTest.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StairwayGamesTest.Common
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;
            
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
            
            DataController.ResetInventory();
        }
    }
}