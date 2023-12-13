using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.playerInformation
{
    public class SetWorldBounds : MonoBehaviour
    {
        void Awake()
        {
            Tilemap map = gameObject.GetComponent<Tilemap>();
            Globals.WorldBounds = map.localBounds;
        }
    }
}

