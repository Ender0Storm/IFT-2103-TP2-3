using System.Collections.Generic;
using Game.Menu;
using UnityEngine;

namespace Game.PlayerInformation
{
    public class ControlsManager : MonoBehaviour
    {
        private Dictionary<string, KeyCode> keyMap;
        
        public void Start()
        {
            keyMap = ControlsStorage._controlsMap;
        }
        public bool IsKeyDown(string keyString)
        {
            KeyCode keyPressed = keyMap[keyString];
            if (Input.GetKeyDown(keyPressed))
            {
                return true;
            }
            return false;
        }
    }
}