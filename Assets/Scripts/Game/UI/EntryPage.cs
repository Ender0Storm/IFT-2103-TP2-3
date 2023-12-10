using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class EntryPage : MonoBehaviour
{
    public UnityEvent onStartButtonClick;
    public UnityEvent onQuitButtonClick;

    public void startButtonClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.MenuSound);
        onStartButtonClick.Invoke();
    }
    public void quitButtonClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.MenuSound);
        onQuitButtonClick.Invoke();
    }
}
