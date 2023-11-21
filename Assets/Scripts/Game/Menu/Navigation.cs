﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Menu
{
    public class Navigation : MonoBehaviour
    {
        private GameObject _startButton;
        private GameObject _singlePlayerButton;
        private GameObject _multiplayerButton;
        private GameObject _optionsButton;
        private GameObject _back21Button;
        private DifficultySelection _difficultySelection;
        
        private GameObject _menuPage1;
        private GameObject _menuPage2;
        private GameObject _optionsPage;
        private GameObject _singlePlayerSettingsPage;
        private string _difficulty;
        void Start()
        {
            _menuPage1 = GameObject.Find("MenuPage1");
            _menuPage2 = GameObject.Find("MenuPage2");
            _optionsPage = GameObject.Find("OptionsPage");
            _singlePlayerSettingsPage = GameObject.Find("SingleplayerSettingsPage");
            _difficultySelection =
                _singlePlayerSettingsPage.GetComponentInChildren<DifficultySelection>();
            
            _startButton = GameObject.Find("Start");
            _singlePlayerButton = GameObject.Find("Singleplayer");
            _multiplayerButton = GameObject.Find("Multiplayer");
            _optionsButton = GameObject.Find("Options");
            _back21Button = GameObject.Find("Back");
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
            _difficultySelection._selectedDifficulty = "none";
            ResetColor();
        }

        public void SinglePlayerSettingsPageToSinglePlayer()
        {
            _difficulty = _difficultySelection._selectedDifficulty;
            if (_difficulty != "none")
            {
                SceneManager.LoadScene("SinglePlayer Scene");
            }
        }

        public void ResetColor()
        {
            foreach(Image element in gameObject.GetComponentsInChildren<Image>())
            {
                element.color = new Color(255,255,255);
            }
        }
    }
}