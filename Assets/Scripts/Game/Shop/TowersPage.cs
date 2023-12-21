using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Shop
{
    public class TowersPage : MonoBehaviour
    {

        [SerializeField]
        private Sprite activeFrame;
        [SerializeField]
        private Sprite inactiveFrame;
        [SerializeField]
        private Image thinCanonFrame;
        [SerializeField]
        private Image bigCanonFrame;
        [SerializeField]
        private Image sphereBaseFrame;
        [SerializeField]
        private Image triangleBaseFrame;
        [SerializeField]
        private Image thinCanonBackground;
        [SerializeField]
        private Image bigCanonBackground;
        [SerializeField]
        private Image sphereBaseBackground;
        [SerializeField]
        private Image triangleBaseBackground;
        [SerializeField]
        private Text thinCanonPrice;
        [SerializeField]
        private Text bigCanonPrice;
        [SerializeField]
        private Text sphereBasePrice;
        [SerializeField]
        private Text triangleBasePrice;
        
        [SerializeField]
        private Image yellowCanonFrame;
        [SerializeField]
        private Image redCanonFrame;
        [SerializeField]
        private Image blueCanonFrame;
        [SerializeField]
        private Image greenCanonFrame;
        
        [SerializeField]
        private Image blackBaseFrame;
        [SerializeField]
        private Image whiteBaseFrame;

        private readonly Color _green = new Color(0, 200, 0);

        private void Start()
        {
            foreach (Content content in ShopContent.GetContentsByType(ContentType.Tower))
            {
                if (content.Name == "ThinCanon") {
                    thinCanonPrice.text = content.Price.ToString();
                    if (content.Unlocked)
                    {
                        thinCanonBackground.color = _green;
                        if (content.Activated)
                        {
                            thinCanonFrame.sprite = activeFrame;
                        }
                    }
                } else if (content.Name == "BigCanon") {
                    bigCanonPrice.text = content.Price.ToString();
                    if (content.Unlocked)
                    {
                        bigCanonBackground.color = _green;
                        if (content.Activated)
                        {
                            bigCanonFrame.sprite = activeFrame;
                        }
                    }
                } else if (content.Name == "SphereBase")
                {
                    sphereBasePrice.text = content.Price.ToString();
                    if (content.Unlocked)
                    {
                        sphereBaseBackground.color = _green;
                        if (content.Activated)
                        {
                            sphereBaseFrame.sprite = activeFrame;
                        }
                    }
                } else if (content.Name == "TriangleBase")
                {
                    triangleBasePrice.text = content.Price.ToString();
                    if (content.Unlocked)
                    {
                        triangleBaseBackground.color = _green;
                        if (content.Activated)
                        {
                            triangleBaseFrame.sprite = activeFrame;
                        }
                    }
                }
            }
        }
        
        public void BuyThinCanonButtonClick(RectTransform button)
        {
            StartCoroutine(TransitionAnimation(button, () =>
            {
                if (ShopContent.BuyContent(ContentType.Tower, "ThinCanon"))
                {
                    thinCanonBackground.color = new Color(0, 200, 0);
                }
            }));
        }

        public void ActiveThinCannonButtonClick(RectTransform button)
        {
            StartCoroutine(TransitionAnimation(button, () =>
            {
                if (ShopContent.ActivateContent(ContentType.Tower, "ThinCanon"))
                {
                    SetInactiveCanonFrames();
                    thinCanonFrame.sprite = activeFrame;
                }
            }));
        }
        
        public void BuyBigCanonButtonClick(RectTransform button)
        {
            StartCoroutine(TransitionAnimation(button, () =>
            {
                if (ShopContent.BuyContent(ContentType.Tower, "BigCanon"))
                {
                    bigCanonBackground.color = new Color(0, 200, 0);
                }
            }));
        }

        public void ActiveBigCannonButtonClick(RectTransform button)
        {
            StartCoroutine(TransitionAnimation(button, () =>
            {
                if (ShopContent.ActivateContent(ContentType.Tower, "BigCanon"))
                {
                    SetInactiveCanonFrames();
                    bigCanonBackground.sprite = activeFrame;
                }
            }));
        }
        
        public void BuySphereBaseButtonClick(RectTransform button)
        {
            StartCoroutine(TransitionAnimation(button, () =>
            {
                if (ShopContent.BuyContent(ContentType.Tower, "SphereBase"))
                {
                    sphereBaseBackground.color = new Color(0, 200, 0);
                }
            }));
        }

        public void ActiveSphereBaseButtonClick(RectTransform button)
        {
            StartCoroutine(TransitionAnimation(button, () =>
            {
                if (ShopContent.ActivateContent(ContentType.Tower, "SphereBase"))
                {
                    SetInactiveBaseFrames();
                    sphereBaseFrame.sprite = activeFrame;
                }
            }));
        }
        
        public void BuyTriangleBaseButtonClick(RectTransform button)
        {
            StartCoroutine(TransitionAnimation(button, () =>
            {
                if (ShopContent.BuyContent(ContentType.Tower, "TriangleBase"))
                {
                    triangleBaseBackground.color = new Color(0, 200, 0);
                }
            }));
        }
        
        public void ActiveTriangleBaseButtonClick(RectTransform button)
        {
            StartCoroutine(TransitionAnimation(button, () =>
            {
                if (ShopContent.ActivateContent(ContentType.Tower, "TriangleBase"))
                {
                    SetInactiveBaseFrames();
                    triangleBaseFrame.sprite = activeFrame;
                }
            }));
        }

        public void ActiveYellowCanonButtonClick(RectTransform button)
        {
            StartCoroutine(TransitionAnimation(button, () =>
            {
                if (ShopContent.ActivateContent(ContentType.Tower, "YellowCanon"))
                {
                    SetInactiveCanonColorFrames();
                    yellowCanonFrame.sprite = activeFrame;
                }
            }));
        }
        
        public void ActiveBlueCanonButtonClick(RectTransform button)
        {
            StartCoroutine(TransitionAnimation(button, () =>
            {
                if (ShopContent.ActivateContent(ContentType.Tower, "BlueCanon"))
                {
                    SetInactiveCanonColorFrames();
                    blueCanonFrame.sprite = activeFrame;
                }
            }));
        }
        
        public void ActiveRedCanonButtonClick(RectTransform button)
        {
            StartCoroutine(TransitionAnimation(button, () =>
            {
                if (ShopContent.ActivateContent(ContentType.Tower, "RedCanon"))
                {
                    SetInactiveCanonColorFrames();
                    redCanonFrame.sprite = activeFrame;
                }
            }));
        }
        
        public void ActiveGreenCanonButtonClick(RectTransform button)
        {
            StartCoroutine(TransitionAnimation(button, () =>
            {
                if (ShopContent.ActivateContent(ContentType.Tower,"GreenCanon"))
                {
                    SetInactiveCanonColorFrames();
                    yellowCanonFrame.sprite = activeFrame;
                }
            }));
        }
        
        public void ActiveBlackBaseButtonClick(RectTransform button)
        {
            StartCoroutine(TransitionAnimation(button, () =>
            {
                if (ShopContent.ActivateContent(ContentType.Tower,"BlackBase"))
                {
                    SetInactiveBaseColorFrames();
                    blackBaseFrame.sprite = activeFrame;
                }
            }));
        }
        
        public void ActiveWhiteBaseButtonClick(RectTransform button)
        {
            StartCoroutine(TransitionAnimation(button, () =>
            {
                if (ShopContent.ActivateContent(ContentType.Tower,"WhiteBase"))
                {
                    SetInactiveBaseColorFrames();
                    whiteBaseFrame.sprite = activeFrame;
                }
            }));
        }
        
        private void SetInactiveCanonFrames()
        {
            thinCanonFrame.sprite = inactiveFrame;
            bigCanonFrame.sprite = inactiveFrame;
        }
        
        private void SetInactiveBaseFrames()
        {
            sphereBaseFrame.sprite = inactiveFrame;
            triangleBaseFrame.sprite = inactiveFrame;
        }
        
        private void SetInactiveCanonColorFrames()
        {
            yellowCanonFrame.sprite = inactiveFrame;
            blueCanonFrame.sprite = inactiveFrame;
            greenCanonFrame.sprite = inactiveFrame;
            yellowCanonFrame.sprite = inactiveFrame;
        }
        
        private void SetInactiveBaseColorFrames()
        {
            whiteBaseFrame.sprite = inactiveFrame;
            blackBaseFrame.sprite = inactiveFrame;
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
            while (Time.time < startTime + duration)
            {
                button.sizeDelta = Vector2.Lerp(originalScale, originalScale * 0.95f, (Time.time - startTime) / duration);
                yield return null;
            }
            button.sizeDelta = originalScale;
        }
    }
}