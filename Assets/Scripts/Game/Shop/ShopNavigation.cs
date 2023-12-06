using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Shop
{
    public class ShopNavigation : MonoBehaviour
    {
        [SerializeField]
        private GameObject _entryPage;
        [SerializeField]
        private GameObject _boardPage;
        [SerializeField]
        private GameObject _ennemiesPage;
        [SerializeField]
        private GameObject _towersPage;
        [SerializeField]
        private GameObject _othersPage;
        private List<Image> buttons;
        public GameObject transitionView;
        private Image transitionImage;

        private void Start()
        {
            transitionImage = transitionView.GetComponentInChildren<Image>();
            transitionView.SetActive(false);
            DeactivateAllPages();
            _entryPage.SetActive(true);
        }

        public void GoEntryPage(RectTransform button)
        {
            StartCoroutine(TransitionAnimation(button, () =>
            {
                DeactivateAllPages();
                _entryPage.SetActive(true);
            }));
        }
        
        public void GoBoardPage(RectTransform button)
        {
            StartCoroutine(TransitionAnimation(button, () =>
            {
                DeactivateAllPages();
                _boardPage.SetActive(true);
            }));
        }
        
        public void GoTowersPage(RectTransform button = null)
        {
            StartCoroutine(TransitionAnimation(button, () =>
            {
                DeactivateAllPages();
                _towersPage.SetActive(true);
            }));
        }

        public void GoEnemiesPage(RectTransform button)
        {
            StartCoroutine(TransitionAnimation(button, () =>
            {
                DeactivateAllPages();
                _ennemiesPage.SetActive(true);
            }));
        }
        
        public void GoOthersPage(RectTransform button)
        {
            StartCoroutine(TransitionAnimation(button, () =>
            {
                DeactivateAllPages();
                _othersPage.SetActive(true);
            }));
        }

        private void DeactivateAllPages()
        {
            ResetColor();
            _entryPage.SetActive(false);
            _boardPage.SetActive(false);
            _towersPage.SetActive(false);
            _ennemiesPage.SetActive(false);
            _othersPage.SetActive(false);
        }
        
        private IEnumerator TransitionAnimation(RectTransform button, Action onAnimationsComplete)
        {
            //yield return ShrinkAnimation(button);
            //yield return FadeInScreenTransition("in");
            onAnimationsComplete?.Invoke();
            //yield return FadeInScreenTransition("out");
            yield return null;
        }
        
        private static IEnumerator ShrinkAnimation(RectTransform button)
        {
            /*if (button == null)
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
            button.sizeDelta = originalScale;*/
            yield return null;
        }
        
        private IEnumerator FadeInScreenTransition(string inOut)
        {
            const float transitionDuration = 0.2f;
            float startTime = Time.time;
            Color startColor = Color.clear;
            Color targetColor = Color.white;

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
            /*foreach(var button in gameObject.GetComponentsInChildren<Image>().ToList())
            {
                button.color = new Color(255,255,255);
            }*/
        }
    }
}