using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.menu
{
    public class Navigation : MonoBehaviour
    {
        public const int MENU_SCENE_INDEX = 0;
        public const int SINGLEPLAYER_SCENE_INDEX = 1;
        public const int MULTIPLAYER_SCENE_INDEX = 2;

        private DifficultySelection _difficultySelection;
        [SerializeField]
        private SceneLoader _sceneLoader;
        
        [SerializeField]
        private GameObject _entryPage;
        [SerializeField]
        private GameObject _menuPage;
        [SerializeField]
        private GameObject _optionsPage;
        [SerializeField]
        private GameObject _singleplayerPage;
        [SerializeField]
        private GameObject _multiplayerPage;
        [SerializeField]
        private GameObject _controlsPage;
        [SerializeField]
        private GameObject _loadingScreen;
        public static string difficulty;
        public static string joinIP;
        private List<Image> buttons;
        
        public GameObject transitionView;
        private Image transitionImage;

        private void Start()
        {
            transitionImage = transitionView.GetComponentInChildren<Image>();
            transitionView.SetActive(false);
            difficulty = "";
            joinIP = "";
            _difficultySelection = _singleplayerPage.GetComponentInChildren<DifficultySelection>();
            
            DeactivateAllPages();
            _entryPage.SetActive(true);

            SoundManager.InitiateMenuMusic();
            StartCoroutine(SoundManager.MusicVolumeFade(new Dictionary<SoundManager.Sound, float> { { SoundManager.Sound.MenuMusic, 0.75f } }));
        }

        public void GoEntryPage(RectTransform button)
        {
            StartCoroutine(TransitionAnimation(button, () =>
            {
                DeactivateAllPages();
                _entryPage.SetActive(true);
            }));
        }
        
        public void GoMenuPage(RectTransform button)
        {
            StartCoroutine(TransitionAnimation(button, () =>
            {
                DeactivateAllPages();
                _menuPage.SetActive(true);
            }));
        }
        
        public void GoOptionsPage(RectTransform button = null)
        {
            StartCoroutine(TransitionAnimation(button, () =>
            {
                DeactivateAllPages();
                _optionsPage.SetActive(true);
            }));
        }

        public void GoControlsPage(RectTransform button)
        {
            StartCoroutine(TransitionAnimation(button, () =>
            {
                DeactivateAllPages();
                _controlsPage.SetActive(true);
            }));
        }
        
        public void GoSingleplayerPage(RectTransform button)
        {
            StartCoroutine(TransitionAnimation(button, () =>
            {
                DeactivateAllPages();
                _singleplayerPage.SetActive(true);
            }));
        }

        public void GoMultiplayerPage(RectTransform button)
        {
            StartCoroutine(TransitionAnimation(button, () =>
            {
                DeactivateAllPages();
                _multiplayerPage.SetActive(true);
            }));
        }

        public void StartSingleplayerGame(RectTransform button)
        {
            StartCoroutine(TransitionAnimation(button, () =>
            {
                DeactivateAllPages();
                difficulty = _difficultySelection.GetSelectedDifficulty();
                _sceneLoader.LoadScene(SINGLEPLAYER_SCENE_INDEX);
            }));
        }
        
        public void StartMultiplayerHost(RectTransform button)
        {
            StartCoroutine(TransitionAnimation(button, () =>
            {
                DeactivateAllPages();
                joinIP = "localhost";
                _sceneLoader.LoadScene(MULTIPLAYER_SCENE_INDEX);
            }));
        }

        public void StartMultiplayerJoin(RectTransform button)
        {
            StartCoroutine(TransitionAnimation(button, () =>
            {
                string input = _multiplayerPage.GetComponentInChildren<TMP_InputField>().text;
                if (input.Length >= 7)
                {
                    DeactivateAllPages();
                    joinIP = input;
                    _sceneLoader.LoadScene(MULTIPLAYER_SCENE_INDEX);
                }
            }));
        }

        public void Quit(RectTransform button)
        {
            Application.Quit();
        }

        private void DeactivateAllPages()
        {
            ResetColor();
            _entryPage.SetActive(false);
            _menuPage.SetActive(false);
            _optionsPage.SetActive(false);
            _singleplayerPage.SetActive(false);
            _multiplayerPage.SetActive(false);
            _controlsPage.SetActive(false);
            _loadingScreen.SetActive(false);
        }
        
        private IEnumerator TransitionAnimation(RectTransform button, Action onAnimationsComplete)
        {
            yield return ShrinkAnimation(button);
            yield return FadeInScreenTransition("in");
            onAnimationsComplete?.Invoke();
            yield return FadeInScreenTransition("out");
        }
        
        private static IEnumerator ShrinkAnimation(RectTransform button)
        {
            if (button == null)
            {
                yield break;
            }
            
            Vector2 originalScale = button.sizeDelta;
            const float duration = 0.1f;
            var startTime = Time.time;

            while (Time.time < startTime + duration)
            {
                button.sizeDelta = Vector2.Lerp(originalScale, originalScale * 0.9f, (Time.time - startTime) / duration);
                yield return null;
            }
            button.sizeDelta = originalScale;
        }
        
        private IEnumerator FadeInScreenTransition(string inOut)
        {
            const float transitionDuration = 0.4f;
            float startTime = Time.time;
            Color startColor = Color.clear;
            Color targetColor = Color.black;

            transitionView.SetActive(true);

            if (inOut == "in")
            {
                while (Time.time < startTime + transitionDuration)
                {
                    float time = (Time.time - startTime) / transitionDuration;
                    transitionImage.color = Color.Lerp(startColor, targetColor, time);
                    yield return null;
                }
            }
            else
            {
                while (Time.time < startTime + transitionDuration)
                {
                    float time = (Time.time - startTime) / transitionDuration;
                    transitionImage.color = Color.Lerp(targetColor, startColor, time);
                    yield return null;
                }
            }
            transitionView.SetActive(false);
        }

        private void ResetColor()
        {
            foreach(var button in gameObject.GetComponentsInChildren<Image>().ToList())
            {
                button.color = new Color(255,255,255);
            }
        }
    }
}