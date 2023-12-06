using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class LoadingScreen : MonoBehaviour
{
    public UnityEvent onValueChanged;

    public void changeValue()
    {
        onValueChanged.Invoke();
    }
}
