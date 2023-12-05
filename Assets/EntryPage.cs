using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class EntryPage : MonoBehaviour
{
    public UnityEvent onStartButtonClick;
    public UnityEvent onQuitButtonClick;

    private Button startButton;
    private Button quitButton;

    private void Start()
    {
        Debug.Log("load");

        //startButton = GameObject.Find("Start").GetComponent<Button>();
        //quitButton = GameObject.Find("Quit").GetComponent<Button>();
    }

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
