using UnityEngine;
using UnityEngine.UI;

public class ChosenTower : MonoBehaviour
{
    [SerializeField]
    private GameObject _towerPrefab;
    [SerializeField]
    private Text _textCost;

    private Tower _tower;
    
    void Start()
    {
        _tower = _towerPrefab.GetComponent<Tower>();
        _textCost.text = _tower.GetCost() + " coins";
    }

    public Tower GetTower()
    {
        return _tower;
    }

    public GameObject GetPrefab()
    {
        return _towerPrefab;
    }
}
