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
        Debug.Log("click");
        onStartButtonClick.Invoke();
    }
    public void quitButtonClick()
    {
        onQuitButtonClick.Invoke();
    }
}
