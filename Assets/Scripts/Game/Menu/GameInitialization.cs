using Game.Shop;
using UnityEngine;

namespace Game.menu
{
    public class GameInitialization : MonoBehaviour
    {
        public void Start()
        {
            ShopContent.Initializing();
        }
    }
}