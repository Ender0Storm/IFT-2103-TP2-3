using UnityEngine;
using UnityEngine.UI;

namespace Game.Shop
{
    public class GemsDisplay : MonoBehaviour
    {
        [SerializeField] 
        private Text gemsText;

        private void Start()
        {
            SetGemsAmount();
        }

        public void SetGemsAmount()
        {
            gemsText.text = ShopContent.getGems().ToString();
        }
    }
}