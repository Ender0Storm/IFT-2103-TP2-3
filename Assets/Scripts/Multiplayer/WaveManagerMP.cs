using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManagerMP : WaveManager
{
    public void SetMPPositions()
    {
        Transform boardTransform = GameObject.Find($"BoardP{Globals.PlayerID}").transform;
        _map = boardTransform.Find("MapBuilder").GetComponent<MapBuilder>();
    }
}
