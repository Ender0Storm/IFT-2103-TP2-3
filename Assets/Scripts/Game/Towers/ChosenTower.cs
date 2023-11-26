using UnityEngine;
using UnityEngine.UI;

namespace Game.towers
{
    public class ChosenTower : MonoBehaviour
    {
        [SerializeField]
        protected GameObject _towerPrefab;
        [SerializeField]
        protected Text _textCost;

        private Tower _tower;
    
        void Start()
        {
            _tower = _towerPrefab.GetComponent<Tower>();
            _textCost.text = $"{GetCost()} coins";
        }

        public Tower GetTower()
        {
            return _tower;
        }

        public GameObject GetPrefab()
        {
            return _towerPrefab;
        }

        public virtual int GetSize()
        {
            return _tower.GetSize();
        }

        public virtual int GetCost()
        {
            return _tower.GetCost();
        }

        public virtual Vector2 CenterOnGrid(Vector2 vector)
        {
            return _tower.CenterOnGrid(vector);
        }
    }
}

