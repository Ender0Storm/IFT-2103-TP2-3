using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Game.menu
{
    public class ControlsInputManager : MonoBehaviour
    {
        public static Dictionary<string, KeyCode> _controlsMap;

        public static ControlsInputManager Instance { get; private set; }
        
        [SerializeField]
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
        
        private const KeyCode DEFAULT_KEY = KeyCode.None;

        private Text _selectText;
        private Text _upText;
        private Text _downText;
        private Text _rightText;
        private Text _leftText;
        private Text _spaceText;
        private Text _escapeText;

        void Awake()
        {
            ControlsStorage.CreateMap();
            
            _controlsMap = ControlsStorage._controlsMap;
        }
        void Start()
        {
            _selectText = selectControl.GetComponentInChildren<Text>();
            _upText = upControl.GetComponentInChildren<Text>();
            _downText = downControl.GetComponentInChildren<Text>();
            _rightText = rightControl.GetComponentInChildren<Text>();
            _leftText = leftControl.GetComponentInChildren<Text>();
            _spaceText = spaceControl.GetComponentInChildren<Text>();
            _escapeText = escapeControl.GetComponentInChildren<Text>();
        }

        public void SetKey(string actionKey, KeyCode keyCode)
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

        private void CheckKeyExists(KeyCode keyPressed)
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
            Debug.Log(_controlsMap.Keys.ToList().ToString());
            foreach (string key in _controlsMap.Keys.ToList())
            {
                Debug.Log(key);
                if (_controlsMap[key] == KeyCode.None)
                {
                    validInputs = false;
                    break;
                }
            }

            if (validInputs)
            {
                ControlsStorage._controlsMap = _controlsMap;
                _navigation.GoOptionsPage();
            }
        }
    }
}