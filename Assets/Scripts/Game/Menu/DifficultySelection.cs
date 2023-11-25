using UnityEngine;
using UnityEngine.UI;

namespace Game.Menu
{
    public class DifficultySelection : MonoBehaviour
    {
        private string _selectedDifficulty;
        private GameObject _easyButton;
        private GameObject _hardButton;

        void Start()
        {
            _easyButton = GameObject.Find("Easy");
            _hardButton = GameObject.Find("Hard");
            _selectedDifficulty = "none";
        }

        public void HardDifficultySelection()
        {
            _selectedDifficulty = "hard";
            _hardButton.GetComponentInChildren<Image>().color = new Color(250, 0 , 0);
            _easyButton.GetComponentInChildren<Image>().color = new Color(255, 255, 255);
        }

        public void EasyDifficultySelection()
        {
            _selectedDifficulty = "easy";
            _hardButton.GetComponentInChildren<Image>().color = new Color(255, 255 , 255);
            _easyButton.GetComponentInChildren<Image>().color = new Color(0, 250, 0);
        }

        public string GetSelectedDifficulty()
        {
            return _selectedDifficulty;
        }

        public void SetSelectedDifficulty(string value)
        {
            _selectedDifficulty = value;
        }
    }
}