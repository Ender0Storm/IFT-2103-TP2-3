  using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Menu
{
    public class ControlsManager : MonoBehaviour
    {
        private Dictionary<string, string> _controlsMap;

        [SerializeField]
        private GameObject menu;

        private Navigation _navigation;
        
        [SerializeField] 
        private GameObject selectionControl;
        [SerializeField]
        private GameObject upControl;
        [SerializeField]
        private GameObject downControl;
        [SerializeField]
        private GameObject rightControl;
        [SerializeField]
        private GameObject leftControl;
        [SerializeField]
        private GameObject escapeControl;
        
        private readonly string DEFAULT_KEY = "";

        private Text _selectionText;
        private Text _upText;
        private Text _downText;
        private Text _rightText;
        private Text _leftText;
        private Text _escapeText;

        public void Start()
        {
            _navigation = menu.GetComponent<Navigation>();
            
            _selectionText = selectionControl.GetComponentInChildren<Text>();
            _upText = upControl.GetComponentInChildren<Text>();
            _downText = downControl.GetComponentInChildren<Text>();
            _rightText = rightControl.GetComponentInChildren<Text>();
            _leftText = leftControl.GetComponentInChildren<Text>();
            _escapeText = escapeControl.GetComponentInChildren<Text>();

            _controlsMap = new Dictionary<string, string>();
        }

        public void SetKey(string actionKey, string keyCode)
        {
            CheckKeyExists(keyCode);
            if (_controlsMap.ContainsKey(actionKey))
            {
                _controlsMap[actionKey] = keyCode;
            }
            else
            {
                _controlsMap.Add(actionKey, keyCode);
            }
            
        }

        private void CheckKeyExists(string keyPressed)
        {
            foreach (string key in _controlsMap.Keys.ToList())
            {
                if (_controlsMap[key] == keyPressed)
                {
                    _controlsMap[key] = DEFAULT_KEY;
                    ClearKey(key);
                    
                }
            }
        }

        private void ClearKey(string actionCode)
        {
            switch (actionCode)
            {
                case "selection":
                {
                    _selectionText.text = "";
                    break;
                }
                case "up":
                {
                    _upText.text = "";
                    break;
                }
                case "down":
                {
                    _downText.text = "";
                    break;
                }
                case "right":
                {
                    _rightText.text = "";
                    break;
                }
                case "left":
                {
                    _leftText.text = "";
                    break;
                }
                case "escape":
                {
                    _escapeText.text = "";
                    break;
                }
            }
        }
        
        public void SaveControls()
        {
            bool validInputs = true;
            foreach (string key in _controlsMap.Keys)
            {
                if (_controlsMap[key] == "")
                {
                    Debug.Log("Une touche n'a pas été renseignée.");
                    validInputs = false;
                    break;
                }
            }

            if (validInputs)
            {
                _navigation.GoOptionsPage();
            }
        }
    }
}