using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Menu
{
    public class KeyBindingManager : MonoBehaviour
    {
        public Button bindButton;
        public GameObject controls;
        
        private ControlsInputManager _controlsInputManager;
        private bool _active = true;
        public string actionKey;
        private Text _text;

        private void Start()
        {
            _controlsInputManager = controls.GetComponent<ControlsInputManager>();
            _text = bindButton.GetComponentInChildren<Text>();
            bindButton.onClick.AddListener(StartKeybinding);
            _text.text = ControlsStorage._controlsMap[actionKey].ToString();
        }

        private void Update()
        {
            if (_active == false && Input.anyKeyDown)
            {
                foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(keyCode))
                    {
                        _controlsInputManager.SetKey(actionKey, keyCode);

                        if (keyCode.ToString().Equals("Mouse0"))
                        {
                            _text.text = "Mouse 0";
                        } else if (keyCode.ToString().Equals("Mouse1"))
                        {
                            _text.text = "Mouse 1";
                        }else if (keyCode.ToString().Equals("Escape"))
                        {
                            _text.text = "ESCAPE";
                        }
                        else
                        {
                            _text.text = keyCode.ToString();
                        }
                        
                        StartCoroutine(EnableButtonAfterDelay());
                    }
                }
            }
        }

        private void StartKeybinding()
        {
            if (_active)
            {
                _text.text = "";
                bindButton.interactable = false;
                _active = false;
            }
        }
        
        private IEnumerator EnableButtonAfterDelay()
        {
            yield return new WaitForSeconds(0.2f);
            bindButton.interactable = true;
            _active = true;
        }
    }
}