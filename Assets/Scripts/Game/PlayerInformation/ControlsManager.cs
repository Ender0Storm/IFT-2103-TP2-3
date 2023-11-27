using System.Collections.Generic;
using Game.menu;
using UnityEngine;

namespace Game.playerInformation
{
    public class ControlsManager : MonoBehaviour
    {
        private Dictionary<string, KeyCode> keyMap;
        
        public void Start()
        {
            keyMap = ControlsStorage.ControlsMap;
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