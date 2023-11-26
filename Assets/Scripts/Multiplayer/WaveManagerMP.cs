using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManagerMP : WaveManager
{
    public void SetMPPositions()
    {
        Transform boardTransform = GameObject.Find($"BoardP{Globals.PlayerID}").transform;
        _portalTransform = boardTransform.Find("Spawn Portal");

        _pathFinding.SetMPPositions(boardTransform);
    }
}
