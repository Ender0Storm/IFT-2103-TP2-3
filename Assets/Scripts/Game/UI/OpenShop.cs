using UnityEngine;

namespace Game.ui
{
    public class OpenShop : MonoBehaviour
    {
        public static bool isShopping;
    
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
            shopMenuUI.SetActive(false);
            Time.timeScale = 1f;
            isShopping = false;
        }
    }
}