using UnityEngine;
using UnityEngine.Events;

namespace Game.Shop
{
    public class EntryPage : MonoBehaviour
    {
        public UnityEvent onStartButtonClick;
        public UnityEvent onQuitButtonClick;

        public void BoardButtonClick()
        {
            onStartButtonClick.Invoke();
        }
        
        public void QuitButtonClick()
        {
            onQuitButtonClick.Invoke();
        }
    }
}