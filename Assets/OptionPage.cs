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

    private Slider volumeSlider;
    private Button controlsButton;
    private Button backButton;

    private void Start()
    {
        volumeSlider = GameObject.Find("Sound Volume").GetComponent< Slider>();
        controlsButton = GameObject.Find("Controls").GetComponent<Button>();
        backButton = GameObject.Find("Back").GetComponent<Button>();
    }


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
