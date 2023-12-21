using Game.playerInformation;
using Game.ui;
using Unity.Netcode;
using UnityEngine;

public class BuildControllerMP : BuildController
{
    protected override bool BuildChecks()
    {
        return Input.GetMouseButtonDown(0) && _currency >= _chosenTower.GetCost() && _hoverHighlight.GetComponent<HighlightChecksMP>().CheckIfClear() && _waveManager.CanBuild() && !PauseMenu.isPaused;
    }

    protected override void Build()
    {
        Vector2 position = _chosenTower.CenterOnGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        GameObject tower = Instantiate(_chosenTower.GetPrefab(), position, Quaternion.identity);
        if (_pathFinding.FindPath(_waveManager.GetMap().GetSpawnPosition(), _waveManager.GetMap().GetTownPosition()) != null)
        {
            _hoverHighlight.GetComponent<ServerRequests>().SpawnTowerServerRpc(_chosenTower.GetPrefab().name, position, NetworkManager.Singleton.LocalClientId);
            _currency -= _chosenTower.GetCost();
        }
        Destroy(tower);
    }

    public void SetHighlight(GameObject highlight)
    {
        _hoverHighlight = highlight;
    }
}
