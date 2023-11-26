using Game.menu;
using Game.playerInformation;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.ui
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool isPaused;
    
        [SerializeField]
        private GameObject _pauseMenuUI;
        [SerializeField]
        private GameObject _endMenuUI;
    
        [SerializeField]
        private GameObject player;
    
        private ControlsManager _controlsManager;
    
        [SerializeField]
        private TMPro.TMP_Text _scoreText;
    
        void Start()
        {
            _controlsManager = player.GetComponent<ControlsManager>();
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    
        public void Resume()
        {
            _pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
    
        private void Pause()
        {
            _pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }
    
        public void EndGame(int waveReached)
        {
            _endMenuUI.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
    
            if (Globals.IsMultiplayer)
            {
                _scoreText.text = $"You lost at wave {waveReached}!";
                _endMenuUI.transform.Find("Try Again Button").gameObject.SetActive(false);
                // Signal other player
            }
            else
            {
                _scoreText.text = $"You reached wave {waveReached}!";
            }
        }
    
        public void LoadMenu()
        {
            Resume();
            if (Globals.IsMultiplayer)
            {
                NetworkManager.Singleton.Shutdown();
                Destroy(NetworkManager.Singleton.gameObject);
            }
    
            SceneManager.LoadScene(Navigation.MENU_SCENE_INDEX);
        }
    
        public void ReloadGame()
        {
            Resume();
            SceneManager.LoadScene(Navigation.SINGLEPLAYER_SCENE_INDEX);
        }
    
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}

