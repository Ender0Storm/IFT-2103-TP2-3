using System.Collections.Generic;
using pathFinding;
using UnityEngine;

namespace enemy
{
    public class MovementManager : MonoBehaviour
    {
        private List<Tile> _path;
        private PathFinding _pathFinding;
        public float speed;
        private Tile _nextTile;
        public int indexPosition;
        private float _directionX;
        private float _directionY;
        public GameObject town;
        private float _nextPositionX;
        private float _nextPositionY;

        public void Start()
        {
            town = GameObject.Find("Town");
            GetPath();
            SetDirection();
        }

        public void Update()
        {
            transform.position = new Vector2(transform.position.x + _directionX, transform.position.y + _directionY);

            if (transform.position.x >= _nextPositionX-0.05 
                && transform.position.y >= _nextPositionY-0.05
                && transform.position.x <= _nextPositionX+0.05
                && transform.position.y <= _nextPositionY+0.05)
            {
                Debug.Log(_nextPositionX + " "+ _nextPositionY);
                transform.position = new Vector2(_nextPositionX, _nextPositionY);
                if (indexPosition == _path.Count - 1)
                {
                    Debug.Log("FINISH");
                    Destroy(this);
                }
                indexPosition++;
                SetDirection();
            }
        }

        private void GetPath()
        {
            Tile start = SetStartTile();
            Tile finish = SetFinishTile();
            start.SetDistance(finish.X, finish.Y);
            
            _pathFinding = new PathFinding(start, finish);
            
            _path = _pathFinding.FindPath();
        }

        private Tile SetStartTile()
        {
            Tile start = new Tile
            {
                X = (int) transform.position.x,
                Y = (int) transform.position.y,
            };
            
            return start;
        }

        private Tile SetFinishTile()
        {
            Tile finish = new Tile
            {
                X = (int)town.transform.position.x,
                Y = (int)town.transform.position.y,
            };
            return finish;
        }

        private void SetDirection()
        {
            _nextTile = _path[indexPosition];
            _nextPositionX = _nextTile.X + 0.5f;
            _nextPositionY = _nextTile.Y + 0.5f;
            _directionX = (_nextPositionX - transform.position.x) / speed;
            _directionY = (_nextPositionY - transform.position.y) / speed;
        }
    }
}
