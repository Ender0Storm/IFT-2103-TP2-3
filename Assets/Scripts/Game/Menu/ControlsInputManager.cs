using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Game.menu
{
    public class ControlsInputManager : MonoBehaviour
    {
        private Dictionary<string, KeyCode> _controlsMap;
        
        private Navigation _navigation;
        
        [SerializeField] 
        private GameObject selectControl;
        [SerializeField]
        private GameObject upControl;
        [SerializeField]
        private GameObject downControl;
        [SerializeField]
        private GameObject rightControl;
        [SerializeField]
        private GameObject leftControl;
        [SerializeField]
        private GameObject spaceControl;
        [SerializeField]
        private GameObject escapeControl;
        
        private const KeyCode DefaultKey = KeyCode.None;

        private Text _selectText;
        private Text _upText;
        private Text _downText;
        private Text _rightText;
        private Text _leftText;
        private Text _spaceText;
        private Text _escapeText;

        private void Awake()
        {
            ControlsStorage.CreateMap();
            
            _controlsMap = ControlsStorage.ControlsMap;
        }
        private void Start()
        {
            _selectText = selectControl.GetComponentInChildren<Text>();
            _upText = upControl.GetComponentInChildren<Text>();
            _downText = downControl.GetComponentInChildren<Text>();
            _rightText = rightControl.GetComponentInChildren<Text>();
            _leftText = leftControl.GetComponentInChildren<Text>();
            _spaceText = spaceControl.GetComponentInChildren<Text>();
            _escapeText = escapeControl.GetComponentInChildren<Text>();
            _navigation = GetComponent<Navigation>();
        }

        public void SetKey(string actionKey, KeyCode keyCode)
        {
            CheckKeyExists(keyCode);
            _controlsMap[actionKey] = keyCode;
        }

        private void CheckKeyExists(KeyCode keyPressed)
        {
            foreach (string key in _controlsMap.Keys.ToList())
            {
                if (_controlsMap[key] == keyPressed)
                {
                    _controlsMap[key] = DefaultKey;
                    ClearKey(key);
                }
            }
        }

        private void ClearKey(string actionCode)
        {
            switch (actionCode)
            {
                case "select":
                {
                    _selectText.text = "";
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
                case "space":
                {
                    _spaceText.text = "";
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
            foreach (string key in _controlsMap.Keys.ToList())
            {
                if (_controlsMap[key] == KeyCode.None)
                {
                    validInputs = false;
                    break;
                }
            }

            if (validInputs)
            {
                ControlsStorage.ControlsMap = _controlsMap;
                _navigation.GoOptionsPage();
            }
        }
    }
}