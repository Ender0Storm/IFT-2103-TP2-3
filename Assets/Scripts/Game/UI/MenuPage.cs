using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuPage : MonoBehaviour
{
    public UnityEvent onSingleplayerButtonClick;
    public UnityEvent onMultiplayerButtonClick;   
    public UnityEvent onOptionsButtonClick;
    public UnityEvent onBackButtonClick;

    public void singleplayerButtonClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.MenuSound);
        onSingleplayerButtonClick.Invoke();
    }
    public void multiplayerButtonClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.MenuSound);
        onMultiplayerButtonClick.Invoke();
    }    
    public void optionsButtonClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.MenuSound);
        onOptionsButtonClick.Invoke();
    }
    public void backButtonClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.MenuSound);
        onBackButtonClick.Invoke();
    }
}
