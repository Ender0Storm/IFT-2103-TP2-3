using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class SingleplayerPage : MonoBehaviour
{
    public UnityEvent onEasyButtonClick;
    public UnityEvent onHardButtonClick;
    public UnityEvent onStartButtonClick;
    public UnityEvent onBackButtonClick;

    public void easyButtonClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.MenuSound);
        onEasyButtonClick.Invoke();
    }
    public void hardButtonClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.MenuSound);
        onHardButtonClick.Invoke();
    }
    public void startButtonClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.MenuSound);
        onStartButtonClick.Invoke();
    }    
    public void backButtonClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.MenuSound);
        onBackButtonClick.Invoke();
    }
}
