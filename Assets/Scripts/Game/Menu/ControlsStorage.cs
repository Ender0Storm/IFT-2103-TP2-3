using System.Collections.Generic;
using UnityEngine;

namespace Game.menu
{
    public static class ControlsStorage
    {
        public static Dictionary<string, KeyCode> ControlsMap;
        private static bool _mapCreated = false;
        
        public static void CreateMap()
        {
            if (!_mapCreated)
            {
                GenerateMap();
                _mapCreated = true;
            }
        }

        private static void GenerateMap()
        {
            ControlsMap = new Dictionary<string, KeyCode>
            {
                {"up", KeyCode.Z},
                {"down", KeyCode.S},
                {"right", KeyCode.D},
                {"left", KeyCode.Q},
                {"space", KeyCode.Space},
                {"select", KeyCode.Mouse0},
                {"escape", KeyCode.Escape}
            };
        }
    }
}