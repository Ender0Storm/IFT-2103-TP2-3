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
        onSingleplayerButtonClick.Invoke();
    }
    public void multiplayerButtonClick()
    {
        onMultiplayerButtonClick.Invoke();
    }    
    public void optionsButtonClick()
    {
        onOptionsButtonClick.Invoke();
    }
    public void backButtonClick()
    {
        onBackButtonClick.Invoke();
    }
}
