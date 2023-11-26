using Game;
using Game.pathFinding;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildController : MonoBehaviour
{
    [SerializeField]
    protected int _currency;
    [SerializeField]
    private ToggleGroup _choices;
    [SerializeField]
    protected WaveManager _waveManager;
    protected ChosenTower _chosenTower;

    [SerializeField]
    protected GameObject _hoverHighlight;

    protected PathFinding _pathFinding;

    public void Start()
    {
        _pathFinding = _waveManager.GetComponent<PathFinding>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_hoverHighlight != null && Application.isFocused)
        { 
            if (_choices.AnyTogglesOn() && !EventSystem.current.IsPointerOverGameObject())
            {
                _chosenTower = _choices.GetFirstActiveToggle().GetComponent<ChosenTower>();

                UpdateHighlight();

                if (BuildChecks())
                {
                    Build();
                }
            }
            else
            {
                _hoverHighlight.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    private void UpdateHighlight()
    {
        int size = _chosenTower.GetSize();

        _hoverHighlight.GetComponent<SpriteRenderer>().enabled = true;
        _hoverHighlight.transform.localScale = new Vector3(size, size, 1);
        _hoverHighlight.transform.position = _chosenTower.CenterOnGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    protected virtual void Build()
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

    protected virtual bool BuildChecks()
    {
        return Input.GetMouseButtonDown(0) && _currency >= _chosenTower.GetCost() && _hoverHighlight.GetComponent<HighlightChecks>().CheckIfClear() && _waveManager.CanBuild() && !PauseMenu.isPaused;
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
