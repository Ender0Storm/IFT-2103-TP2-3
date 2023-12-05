using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class LoadingScreen : MonoBehaviour
{
    public UnityEvent onValueChanged;

    private Slider loadingBar;

    private void Start()
    {
        loadingBar = GetComponentInChildren<Slider>();
    }


    public void changeValue()
    {
        onValueChanged.Invoke();
    }
}
