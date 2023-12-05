using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class MultiplayerPage : MonoBehaviour
{
    public UnityEvent onJoinButtonClick;
    public UnityEvent onHostButtonClick;
    public UnityEvent onBackButtonClick;

    private Button joinButton;
    private Button hostButton;
    private Button backButton;

    private void Start()
    {
        joinButton = GameObject.Find("Join").GetComponent<Button>();
        hostButton = GameObject.Find("Host").GetComponent<Button>();
        backButton = GameObject.Find("Back").GetComponent<Button>();
    }

    public void joinButtonClick()
    {
        onJoinButtonClick.Invoke();
    }    
    public void hostButtonClick()
    {
        onHostButtonClick.Invoke();
    }    
    public void backButtonClick()
    {
        onBackButtonClick.Invoke();
    }
}
