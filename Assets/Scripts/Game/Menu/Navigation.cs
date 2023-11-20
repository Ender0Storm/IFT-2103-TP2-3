using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Menu
{
    public class Navigation : MonoBehaviour
    {
        private GameObject _optionsButton;
        private GameObject _backDifficultyButton;
        private GameObject _optionsReturn;
        private GameObject _menuPage1;
        private GameObject _menuPage2;
        private GameObject _optionsPage;
        private GameObject _gameSettingsPage;
        public static string SelectedDifficulty;
        void Start()
        {
            _menuPage1 = GameObject.Find("Menu Page 1");
            _menuPage2 = GameObject.Find("Menu Page 2");
            _optionsPage = GameObject.Find("Options Page");
            _gameSettingsPage = GameObject.Find("Game Settings Page");
            InitUIVisibility();
        }
        public void MenuPage1ToMenuPage2()
        {
            _menuPage1.SetActive(false);
            _menuPage2.SetActive(true);
            ResetColor();
        }

        public void MenuPage2ToMenuPage1()
        {
            _menuPage1.SetActive(true);
            _menuPage2.SetActive(false);
            ResetColor();
        }

        public void MenuPage2ToOptionsPage()
        {
            _menuPage2.SetActive(false);
            _optionsPage.SetActive(true);
            ResetColor();
        }

        public void OptionsPageToMenuPage2()
        {
            _optionsPage.SetActive(false);
            _menuPage2.SetActive(true);
            ResetColor();
        }

        public void MenuPage2ToGameSettingsPage()
        {
            _menuPage2.SetActive(false);
            _gameSettingsPage.SetActive(true);
            ResetColor();
        }

        public void GameSettingsPageToMenuPage2()
        {
            _gameSettingsPage.SetActive(false);
            _menuPage2.SetActive(true);
            ResetColor();
        }

        private void InitUIVisibility()
        {
            _menuPage1.SetActive(true);
            _menuPage2.SetActive(false);
            _optionsPage.SetActive(false);
            _gameSettingsPage.SetActive(false);
        }

        public void MenuPage2ToSinglePlayer()
        {
            SelectedDifficulty = _gameSettingsPage.GetComponent<DifficultySelection>().selectedDifficulty;
            if (SelectedDifficulty != "none")
            {
                SceneManager.LoadScene("SinglePlayer Scene");
            }
        }

        private void ResetColor()
        {
            foreach (var button in gameObject.GetComponentsInChildren<Image>())
            {
                button.color = new Color(255, 255, 255);
            }
        }
    }
}