using System.Collections.Generic;
using UnityEngine;

namespace Game.Menu
{
    public static class ControlsStorage
    {
        public static Dictionary<string, KeyCode> _controlsMap;
        public static bool mapCreated = false;
        
        public static void CreateMap()
        {
            if (!mapCreated)
            {
                GenerateMap();
                
                mapCreated = true;
            }
        }

        public static void GenerateMap()
        {
            _controlsMap = new Dictionary<string, KeyCode>();
            _controlsMap.Add("up", KeyCode.Z);
            _controlsMap.Add("down", KeyCode.S);
            _controlsMap.Add("right", KeyCode.D);
            _controlsMap.Add("left", KeyCode.Q);
            _controlsMap.Add("space", KeyCode.Space);
            _controlsMap.Add("select", KeyCode.Mouse0);
            _controlsMap.Add("escape", KeyCode.Escape);
        }
    }
}