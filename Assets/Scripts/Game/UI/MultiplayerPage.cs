using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class MultiplayerPage : MonoBehaviour
{
    public UnityEvent onJoinButtonClick;
    public UnityEvent onHostButtonClick;
    public UnityEvent onBackButtonClick;

    public void joinButtonClick()
    {
        onJoinButtonClick.Invoke();
    }    
    public void hostButtonClick()
    {
        onHostButtonClick.Invoke();
    }    
    public void backButtonClick()
    {
        onBackButtonClick.Invoke();
    }
}
