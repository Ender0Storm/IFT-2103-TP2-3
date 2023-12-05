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

    private Button easyButton;
    private Button hardButton;
    private Button startButton;
    private Button backButton;

    private void Start()
    {
        easyButton = GameObject.Find("Easy").GetComponent<Button>();
        hardButton = GameObject.Find("Hard").GetComponent<Button>();
        startButton = GameObject.Find("Start").GetComponent<Button>();
        backButton = GameObject.Find("Back").GetComponent<Button>();
    }

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
