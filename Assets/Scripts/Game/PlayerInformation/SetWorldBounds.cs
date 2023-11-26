using UnityEngine;

namespace Game.playerInformation
{
    public class SetWorldBounds : MonoBehaviour
    {
        void Awake()
        {
            Bounds bounds = new Bounds(transform.position, transform.localScale);
            Globals.WorldBounds = bounds;
        }
    }
}

