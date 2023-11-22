using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField]
    private WaveManager waveManager;
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waveManager.CanBuild())
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }

    public void StartWave()
    {
        waveManager.StartWave();
    }
}
