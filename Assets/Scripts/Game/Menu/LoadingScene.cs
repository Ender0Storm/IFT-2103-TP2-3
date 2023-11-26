using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.menu
{
    public class LoadingScene : MonoBehaviour
    {
        public GameObject loadingScreen;
        public Slider slider;

        public void LoadScene(int scene)
        {
            StartCoroutine(LoadSceneAsync(scene));
        }

        IEnumerator LoadSceneAsync(int sceneId)
        {
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