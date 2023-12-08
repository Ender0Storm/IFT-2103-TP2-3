using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class OptionPage : MonoBehaviour
{
    public UnityEvent onSoundVolumeChanged;
    public UnityEvent onControlsButtonClick;
    public UnityEvent onBackButtonClick;

    public void changeVolume()
    {
        onSoundVolumeChanged.Invoke();
    }
    public void controlsButtonClick()
    {
        onControlsButtonClick.Invoke();
    }   
    public void backButtonClick()
    {
        onBackButtonClick.Invoke();
    }
}
