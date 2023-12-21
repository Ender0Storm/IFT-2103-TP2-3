using System.Collections.Generic;
using Game.Shop;
using UnityEngine;

namespace Game.ui
{
    public class OpenShop : MonoBehaviour
    {
        public static bool isShopping;
        public SpriteRenderer customBoard;
        public Sprite woodBoard;
        public Sprite digitalBoard;
    
        [SerializeField]
        private GameObject shopMenuUI;

        private void Start()
        {
            shopMenuUI.SetActive(false);
        }
    
        public void GoShop()
        {
            shopMenuUI.SetActive(true);
            Time.timeScale = 0f;
            isShopping = true;
        }
    
        public void Resume()
        {
            UpdateContent();
            shopMenuUI.SetActive(false);
            Time.timeScale = 1f;
            isShopping = false;
        }

        private void UpdateContent()
        {
            List<Content> boards = ShopContent.GetContentsByType(ContentType.Board);
            bool customBoardActive = false;
            foreach (Content board in boards)
            {
                if (board.Activated)
                {
                    if (board.Name == "Wood")
                    {
                        customBoard.sprite = woodBoard;
                        customBoardActive = true;
                        break;
                    } else if (board.Name == "Digital")
                    {
                        customBoard.sprite = digitalBoard;
                        customBoardActive = true;
                        break;
                    }
                }
            }

            if (!customBoardActive)
            {
                customBoard = null;
            }
        }
    }
}