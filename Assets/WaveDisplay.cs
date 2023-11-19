using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDisplay : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text _waveText;
    [SerializeField]
    private WaveManager _waveManager;

    // Update is called once per frame
    void Update()
    {
        _waveText.text = "Wave: " + _waveManager.GetWave().ToString();
    }
}
