using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PausePage : MonoBehaviour
{
    public UnityEvent onMenuButtonClick;
    public UnityEvent onQuitButtonClick;
    public UnityEvent onResumeButtonClick;

    public void menuButtonClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.MenuSound);
        onMenuButtonClick.Invoke();
    }
    public void quitButtonClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.MenuSound);
        onQuitButtonClick.Invoke();
    }
    public void resumeButtonClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.MenuSound);
        onResumeButtonClick.Invoke();
    }
}
