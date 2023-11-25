using Game;
using Game.pathFinding;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildController : MonoBehaviour
{
    [SerializeField]
    private int _currency;
    [SerializeField]
    private ToggleGroup _choices;
    [SerializeField]
    private WaveManager _waveManager;
    private ChosenTower _chosenTower;

    [SerializeField]
    private GameObject _hoverHighlight;
    [SerializeField]
    private Transform _towerParent;

    private PathFinding _pathFinding;

    public void Start()
    {
        _pathFinding = _waveManager.GetComponent<PathFinding>();
    }
    // Update is called once per frame
    void Update()
    {
        if (_choices.AnyTogglesOn() && !EventSystem.current.IsPointerOverGameObject())
        {
            _chosenTower = _choices.GetFirstActiveToggle().GetComponent<ChosenTower>();
            Tower towerScript = _chosenTower.GetTower();
            int size = towerScript.GetSize();

            _hoverHighlight.SetActive(true);
            _hoverHighlight.transform.localScale = new Vector3(size, size, 1);
            _hoverHighlight.transform.position = towerScript.CenterOnGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            if (Input.GetMouseButtonDown(0) && _currency >= towerScript.GetCost() && _hoverHighlight.GetComponent<HighlightChecks>().CheckIfClear() && _waveManager.CanBuild() && !PauseMenu.isPaused)
            {
                GameObject tower = Instantiate(_chosenTower.GetPrefab(), towerScript.CenterOnGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition)), Quaternion.identity, _towerParent);
                if (_pathFinding.FindPath() != null)
                {
                    tower.GetComponent<Tower>().SetOwner(this);
                    _currency -= towerScript.GetCost();
                }
                else
                {
                    Destroy(tower);
                }
            }
        }
        else
        {
            _hoverHighlight.SetActive(false);
        }
    }

    public int GetCurrency()
    {
        return _currency;
    }

    public void AddCurrency(int ammount)
    {
        _currency += ammount;
    }
}
