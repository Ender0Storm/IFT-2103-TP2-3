using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Menu
{
    public class Navigation : MonoBehaviour
    {
        private GameObject _startButton;
        private GameObject _singleplayerButton;
        private GameObject _multiplayerButton;
        private GameObject _optionsButton;
        private GameObject _back21Button;
        void Start()
        {
            _startButton = GameObject.Find("Start");
            _singleplayerButton = GameObject.Find("Singleplayer");
            _multiplayerButton = GameObject.Find("Multiplayer");
            _optionsButton = GameObject.Find("Options");
            _back21Button = GameObject.Find("Back");
            MenuPage2ToMenuPage1();
        }
        public void MenuPage1ToMenuPage2()
        {
            _startButton.SetActive(false);
            _singleplayerButton.SetActive(true);
            _multiplayerButton.SetActive(true);
            _optionsButton.SetActive(true);
            _back21Button.SetActive(true);
            ResetColor();
        }

        public void MenuPage2ToMenuPage1()
        {
            _singleplayerButton.SetActive(false);
            _multiplayerButton.SetActive(false);
            _optionsButton.SetActive(false);
            _back21Button.SetActive(false);
            _startButton.SetActive(true);
            ResetColor();
        }

        public void MenuPage2ToSinglePlayer()
        {
            SceneManager.LoadScene("SinglePlayer Scene");
        }

        public void ResetColor()
        {
            _back21Button.GetComponentInChildren<Image>().color = new Color(255,255,255);
            _startButton.GetComponentInChildren<Image>().color = new Color(255,255,255);
                
        }
    }
}