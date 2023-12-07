using System;
using System.Collections;
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

        private readonly Color _green = new Color(0, 200, 0);

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
        
        public void BuyWoodButtonClick(RectTransform button)
        {
            StartCoroutine(TransitionAnimation(button, () =>
            {
                if (ShopContent.BuyContent(ContentType.Board, "Wood"))
                {
                    woodBackground.color = new Color(0, 200, 0);
                }
            }));
        }

        public void ActiveWoodButtonClick(RectTransform button)
        {
            StartCoroutine(TransitionAnimation(button, () =>
            {
                if (ShopContent.ActivateContent(ContentType.Board, "Wood"))
                {
                    SetInactiveFrames();
                    woodFrame.sprite = activeFrame;
                }
            }));
        }
        
        public void BuyDigitalButtonClick(RectTransform button)
        {
            StartCoroutine(TransitionAnimation(button, () =>
            {
                if (ShopContent.BuyContent(ContentType.Board, "Digital"))
                {
                    digitalBackground.color = new Color(0, 200, 0);
                }
            }));
        }
        
        public void ActiveDigitalButtonClick(RectTransform button)
        {
            StartCoroutine(TransitionAnimation(button, () =>
            {
                if (ShopContent.ActivateContent(ContentType.Board, "Digital"))
                {
                    SetInactiveFrames();
                    digitalFrame.sprite = activeFrame;
                }
            }));
        }

        private void SetInactiveFrames()
        {
            digitalFrame.sprite = inactiveFrame;
            woodFrame.sprite = inactiveFrame;
        }
        
        private static IEnumerator TransitionAnimation(RectTransform button, Action onAnimationsComplete)
        {
            yield return ShrinkAnimation(button);
            onAnimationsComplete?.Invoke();
            Time.timeScale = 0f;
        }
        
        private static IEnumerator ShrinkAnimation(RectTransform button)
        {
            if (button == null)
            {
                yield break;
            }
            
            Vector2 originalScale = button.sizeDelta;
            const float duration = 0.1f;
            Time.timeScale = 1f;
            var startTime = Time.time;
            while (startTime < duration)
            {
                button.sizeDelta = Vector2.Lerp(originalScale, originalScale * 0.95f, (Time.time - startTime) / duration);
                yield return null;
            }
            button.sizeDelta = originalScale;
        }
    }
}