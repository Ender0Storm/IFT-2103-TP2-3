using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.playerInformation
{
    public class SetWorldBounds : MonoBehaviour
    {
        public bool active;
        [SerializeField]
        private Tilemap map;
        void Awake()
        {
            if (active)
            {
                Globals.WorldBounds = map.localBounds;
            }

        }
    }
}

