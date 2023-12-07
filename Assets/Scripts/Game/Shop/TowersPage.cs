using UnityEngine;
using UnityEngine.Events;

namespace Game.Shop
{
    public class TowersPage : MonoBehaviour
    {
        public UnityEvent onStartButtonClick;
        public UnityEvent onQuitButtonClick;

        public void StartButtonClick()
        {
            onStartButtonClick.Invoke();
        }
        public void QuitButtonClick()
        {
            onQuitButtonClick.Invoke();
        }
    }
}