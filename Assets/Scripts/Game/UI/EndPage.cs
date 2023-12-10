using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndPage : MonoBehaviour
{
    public UnityEvent onTryAgainButtonClick;
    public UnityEvent onMenuButtonClick;

    public void tryAgainButtonClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.MenuSound);
        onTryAgainButtonClick.Invoke();
    }
    public void menuButtonClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.MenuSound);
        onMenuButtonClick.Invoke();
    }
}
