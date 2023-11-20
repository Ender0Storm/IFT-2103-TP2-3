using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.Menu
{
    public class DifficultySelection : MonoBehaviour
    {
        public string selectedDifficulty = "none";
        private Image _easyImage;
        private Image _hardImage;
        private GameObject _easyButton;
        private GameObject _hardButton;

        void Start()
        {
            _easyButton = GameObject.Find("Easy");
            _hardButton = GameObject.Find("Hard");
            _easyImage = _easyButton.GetComponentInChildren<Image>(_easyButton);
            _hardImage = _hardButton.GetComponentInChildren<Image>(_hardButton);
        }

        public void SetDifficultyEasy()
        {
            selectedDifficulty = "easy";
            ChangeColor();
        }

        public void SetDifficultyHard()
        {
            selectedDifficulty = "hard";
            ChangeColor();
        }

        private void ChangeColor()
        {
            Debug.Log(selectedDifficulty);
            if (selectedDifficulty == "easy")
            {
                _easyImage.color = new Color(0,180,0);
                _hardImage.color = new Color(255,255,255);
            }
            else
            {
                _hardImage.color = new Color(200,0,0);
                _easyImage.color = new Color(255,255,255);
            }
        }

        public void ResetColor()
        {
            _hardImage.color = new Color(255,255,255);
            _easyImage.color = new Color(255,255,255);
            selectedDifficulty = "none";
        }
    }
}