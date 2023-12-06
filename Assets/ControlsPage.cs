using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControlsPage : MonoBehaviour
{
    public UnityEvent onSelectButtonClick;
    public UnityEvent onUpButtonClick; 
    public UnityEvent onDownButtonClick;
    public UnityEvent onRightButtonClick; 
    public UnityEvent onLeftButtonClick;
    public UnityEvent onPauseMenuButtonClick; 
    public UnityEvent onLaunchWaveButtonClick;
    public UnityEvent onSaveButtonClick;

    public void selectButtonClick()
    {
        onSelectButtonClick.Invoke();
    }
    public void upButtonClick()
    {
        onUpButtonClick.Invoke();
    }    
    public void downButtonClick()
    {
        onDownButtonClick.Invoke();
    }
    public void rightButtonClick()
    {
        onRightButtonClick.Invoke();
    }    
    public void leftButtonClick()
    {
        onLeftButtonClick.Invoke();
    }
    public void pauseMenuButtonClick()
    {
        onPauseMenuButtonClick.Invoke();
    }    
    public void launchWaveButtonClick()
    {
        onLaunchWaveButtonClick.Invoke();
    }
    public void saveButtonClick()
    {
        onSaveButtonClick.Invoke();
    }
}
