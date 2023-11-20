using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Menu
{
    public class AnimateButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Image _image;
        private Color _whenPointedColor;

        void Start()
        {
            _image = GetComponentInChildren<Image>();
            _whenPointedColor = new Color(0, 250, 0);
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _image.color = _whenPointedColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_image.color == _whenPointedColor)
                _image.color = new Color(255,255,255);
        }
    }
}