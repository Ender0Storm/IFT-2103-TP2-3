using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnScreenControls : MonoBehaviour
{
    public UnityEvent onStartWaveClick;

    public void startWaveButtonClick()
    {
        onStartWaveClick.Invoke();
    }
}
