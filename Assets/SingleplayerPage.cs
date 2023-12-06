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
        onEasyButtonClick.Invoke();
    }
    public void hardButtonClick()
    {
        onHardButtonClick.Invoke();
    }
    public void startButtonClick()
    {
        onStartButtonClick.Invoke();
    }    
    public void backButtonClick()
    {
        onBackButtonClick.Invoke();
    }
}
