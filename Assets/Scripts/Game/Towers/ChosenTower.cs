using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChosenTower : MonoBehaviour
{
    [SerializeField]
    private GameObject _towerPrefab;

    private Tower _tower;

    // Start is called before the first frame update
    void Start()
    {
        _tower = _towerPrefab.GetComponent<Tower>();
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
