using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Shop
{
    public class BoardPage : MonoBehaviour
    {

        [SerializeField]
        private Sprite activeFrame;
        [SerializeField]
        private Sprite inactiveFrame;
        [SerializeField]
        private Image woodFrame;
        [SerializeField]
        private Image digitalFrame;
        [SerializeField]
        private Image woodBackground;
        [SerializeField]
        private Image digitalBackground;
        [SerializeField]
        private Text woodPrice;
        [SerializeField]
        private Text digitalPrice;

        private Color _green = new Color(0, 200, 0);

        private void Start()
        {
            foreach (Content content in ShopContent.GetContentsByType(ContentType.Board))
            {
                if (content.Name == "Wood")
                {
                    woodPrice.text = content.Price.ToString();
                    if (content.Unlocked)
                    {
                        woodBackground.color = _green;
                        if (content.Activated)
                        {
                            woodFrame.sprite = activeFrame;
                        }
                    }
                }
                else if (content.Name == "Digital")
                {
                    digitalPrice.text = content.Price.ToString();
                    if (content.Unlocked)
                    {
                        digitalBackground.color = _green;
                        if (content.Activated)
                        {
                            digitalFrame.sprite = activeFrame;
                        }
                    }
                }
            }
        }
        
        public void BuyWoodButtonClick()
        {
            if (ShopContent.BuyContent(ContentType.Board, "Wood"))
            {
                woodBackground.color = new Color(0, 200, 0);
            }
        }

        public void ActiveWoodButtonClick()
        {
            if (ShopContent.ActivateContent(ContentType.Board, "Wood"))
            {
                SetInactiveFrames();
                woodFrame.sprite = activeFrame;
            }
        }
        
        public void BuyDigitalButtonClick()
        {
            if (ShopContent.BuyContent(ContentType.Board, "Digital"))
            {
                digitalBackground.color = new Color(0, 200, 0);
            }
        }
        
        public void ActiveDigitalButtonClick()
        {
            if (ShopContent.ActivateContent(ContentType.Board, "Digital"))
            {
                SetInactiveFrames();
                digitalFrame.sprite = activeFrame;
            }
        }

        private void SetInactiveFrames()
        {
            digitalFrame.sprite = inactiveFrame;
            woodFrame.sprite = inactiveFrame;
        }
    }
}