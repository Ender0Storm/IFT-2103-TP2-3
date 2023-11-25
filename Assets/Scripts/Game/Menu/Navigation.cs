using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Menu
{
    public class Navigation : MonoBehaviour
    {
        private DifficultySelection _difficultySelection;
        
        private GameObject _menuPage1;
        private GameObject _menuPage2;
        private GameObject _optionsPage;
        private GameObject _singlePlayerSettingsPage;
        public static string Difficulty;
        void Start()
        {
            _menuPage1 = GameObject.Find("MenuPage1");
            _menuPage2 = GameObject.Find("MenuPage2");
            _optionsPage = GameObject.Find("OptionsPage");
            _singlePlayerSettingsPage = GameObject.Find("SingleplayerSettingsPage");
            _difficultySelection =
                _singlePlayerSettingsPage.GetComponentInChildren<DifficultySelection>();
            MenuInit();
        }

        private void MenuInit()
        {
            _menuPage1.SetActive(true);
            _menuPage2.SetActive(false);
            _optionsPage.SetActive(false);
            _singlePlayerSettingsPage.SetActive(false);
        }
        
        public void MenuPage1ToMenuPage2()
        {
            _menuPage1.SetActive(false);
            _menuPage2.SetActive(true);
            ResetColor();
        }

        public void MenuPage2ToMenuPage1()
        {
            _menuPage2.SetActive(false);
            _menuPage1.SetActive(true);
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
        
        public void MenuPage2SinglePlayerSettingsPage()
        {
            _menuPage2.SetActive(false);
            _singlePlayerSettingsPage.SetActive(true);
            ResetColor();
        }
        
        public void SinglePlayerSettingsPageToMenuPage2()
        {
            _singlePlayerSettingsPage.SetActive(false);
            _menuPage2.SetActive(true);
            _difficultySelection.SetSelectedDifficulty("none");
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