using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Menu
{
    public class LoadingScene : MonoBehaviour
    {
        public GameObject loadingScreen;
        public Image loadingBarFill;
        public GameObject menu;
        public Slider slider;
        private DifficultySelection _difficultySelection;
        
        public static string Difficulty;

        public void LoadScene(int scene)
        {
            _difficultySelection = GameObject.Find("SingleplayerPage").GetComponent<DifficultySelection>();
            Difficulty = _difficultySelection.GetSelectedDifficulty();
            if (Difficulty != "none")
            {
                StartCoroutine(LoadSceneAsync(scene));
            }
        }

        IEnumerator LoadSceneAsync(int sceneId)
        {
            menu.SetActive(false);
            loadingScreen.SetActive(true);
            slider.value = 0.3f;
            yield return new WaitForSeconds(0.5f);
            slider.value = 0.6f;
            yield return new WaitForSeconds(0.5f);
            AsyncOperation loading = SceneManager.LoadSceneAsync(sceneId);
            
            while (!loading.isDone)
            {
                float progressValue = Mathf.Clamp01((loading.progress / 0.9f));

                slider.value = progressValue;
                
                yield return null;
            }
        }
    }
}