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
        onMenuButtonClick.Invoke();
    }
    public void quitButtonClick()
    {
        onQuitButtonClick.Invoke();
    }
    public void resumeButtonClick()
    {
        onResumeButtonClick.Invoke();
    }
}
