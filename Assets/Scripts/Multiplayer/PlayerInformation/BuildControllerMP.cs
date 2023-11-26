using Game.playerInformation;
using Game.towers;
using Game.ui;
using UnityEngine;

public class BuildControllerMP : BuildController
{
    protected override bool BuildChecks()
    {
        return Input.GetMouseButtonDown(0) && _currency >= _chosenTower.GetCost() && _hoverHighlight.GetComponent<HighlightChecksMP>().CheckIfClear() && _waveManager.CanBuild() && !PauseMenu.isPaused;
    }

    protected override void Build()
    {
        GameObject tower = Instantiate(_chosenTower.GetPrefab(), _chosenTower.CenterOnGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition)), Quaternion.identity);
        if (_pathFinding.FindPath() != null)
        {
            tower.GetComponent<Tower>().SetOwner(this);
            _currency -= _chosenTower.GetCost();
        }
        else
        {
            Destroy(tower);
        }
    }

    public void SetHighlight(GameObject highlight)
    {
        _hoverHighlight = highlight;
    }
}
