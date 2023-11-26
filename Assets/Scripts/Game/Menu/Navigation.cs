using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Menu
{
    public class Navigation : MonoBehaviour
    {
        public const int MENU_SCENE_INDEX = 0;
        public const int SINGLEPLAYER_SCENE_INDEX = 1;
        public const int MULTIPLAYER_SCENE_INDEX = 2;

        private DifficultySelection _difficultySelection;
        [SerializeField]
        private LoadingScene _loadingScene;
        
        [SerializeField]
        private GameObject _entryPage;
        [SerializeField]
        private GameObject _menuPage;
        [SerializeField]
        private GameObject _optionsPage;
        [SerializeField]
        private GameObject _singleplayerPage;
        [SerializeField]
        private GameObject _multiplayerPage;
        [SerializeField]
        private GameObject _controlsPage;
        [SerializeField]
        private GameObject _loadingScreen;
        public static string difficulty;
        public static string joinIP;

        void Start()
        {
            difficulty = "";
            joinIP = "";
            _difficultySelection =
                _singleplayerPage.GetComponentInChildren<DifficultySelection>();
            GoEntryPage();
        }

        public void GoEntryPage()
        {
            DeactivateAllPages();
            _entryPage.SetActive(true);
        }
        
        public void GoMenuPage()
        {
            DeactivateAllPages();
            _menuPage.SetActive(true);
        }
        
        public void GoOptionsPage()
        {
            DeactivateAllPages();
            _optionsPage.SetActive(true);
        }

        public void GoControlsPage()
        {
            DeactivateAllPages();
            _controlsPage.SetActive(true);
        }
        
        public void GoSingleplayerPage()
        {
            DeactivateAllPages();
            _singleplayerPage.SetActive(true);
        }

        public void GoMultiplayerPage()
        {
            DeactivateAllPages();
            _multiplayerPage.SetActive(true);
        }

        public void StartSingleplayerGame()
        {
            DeactivateAllPages();
            difficulty = _difficultySelection.GetSelectedDifficulty();
            _loadingScene.LoadScene(SINGLEPLAYER_SCENE_INDEX);
        }
        
        public void StartMultiplayerHost()
        {
            DeactivateAllPages();
            joinIP = "localhost";
            _loadingScene.LoadScene(MULTIPLAYER_SCENE_INDEX);
        }

        public void StartMultiplayerJoin()
        {
            DeactivateAllPages();
            string input = _multiplayerPage.GetComponentInChildren<TMP_InputField>().text;
            if (input.Length >= 7)
            {
                joinIP = input;
                _loadingScene.LoadScene(MULTIPLAYER_SCENE_INDEX);
            }
        }

        public void Quit()
        {
            Application.Quit();
        }

        private void DeactivateAllPages()
        {
            _entryPage.SetActive(false);
            _menuPage.SetActive(false);
            _optionsPage.SetActive(false);
            _singleplayerPage.SetActive(false);
            _multiplayerPage.SetActive(false);
            _controlsPage.SetActive(false);
            _loadingScreen.SetActive(false);
            ResetColor();
        }

        private void ResetColor()
        {
            foreach(var element in gameObject.GetComponentsInChildren<Image>())
            {
                element.color = new Color(255,255,255);
            }
        }
    }
}