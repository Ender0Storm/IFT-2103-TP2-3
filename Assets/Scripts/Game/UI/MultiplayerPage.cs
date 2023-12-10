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
        SoundManager.PlaySound(SoundManager.Sound.MenuSound);
        onJoinButtonClick.Invoke();
    }    
    public void hostButtonClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.MenuSound);
        onHostButtonClick.Invoke();
    }    
    public void backButtonClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.MenuSound);
        onBackButtonClick.Invoke();
    }
}
