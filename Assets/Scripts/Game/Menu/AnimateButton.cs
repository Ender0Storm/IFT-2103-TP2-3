﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.menu
{
    public class AnimateButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Image _image;

        void Start()
        {
            _image = GetComponentInChildren<Image>();
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_image.color != new Color(250, 0, 0) && _image.color != new Color(0, 250, 0))
            {
                _image.color = new Color(0,150,0);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_image.color != new Color(0, 250, 0) && _image.color != new Color(250, 0, 0))
            {
                _image.color = new Color(255,255,255);
            }
        }
    }
}