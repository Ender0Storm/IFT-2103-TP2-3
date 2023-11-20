using Game;
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

    // Update is called once per frame
    void Update()
    {
        if (_choices.AnyTogglesOn() && !EventSystem.current.IsPointerOverGameObject())
        {
            _chosenTower = _choices.GetFirstActiveToggle().GetComponent<ChosenTower>();
            Tower tower = _chosenTower.GetTower();
            int size = tower.GetSize();

            _hoverHighlight.SetActive(true);
            _hoverHighlight.transform.localScale = new Vector3(size, size, 1);
            _hoverHighlight.transform.position = tower.CenterOnGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            
            if (Input.GetMouseButtonDown(0) && _currency >= tower.GetCost() && _hoverHighlight.GetComponent<HighlightChecks>().CheckIfClear() && _waveManager.CanBuild())
            {
                Instantiate(_chosenTower.GetPrefab(), tower.CenterOnGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition)), Quaternion.identity, _towerParent);
                _currency -= tower.GetCost();
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
}
