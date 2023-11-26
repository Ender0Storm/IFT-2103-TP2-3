using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Menu
{
    public class KeyBindingManager : MonoBehaviour
    {
        public Button bindButton;
        public GameObject controls;
        
        private ControlsManager _controlsManager;
        private bool _active = true;
        private string _actionKey;
        private Text _text;

        private void Start()
        {
            _controlsManager = controls.GetComponent<ControlsManager>();
            _text = bindButton.GetComponentInChildren<Text>();
            bindButton.onClick.AddListener(StartKeybinding);
        }

        private void Update()
        {
            if (_active == false && Input.anyKeyDown)
            {
                foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(keyCode))
                    {
                        if (_active == false)
                        {
                            _controlsManager.SetKey(_actionKey, keyCode.ToString());

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
                        }
                        
                        _active = true;
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
        }
    }
}