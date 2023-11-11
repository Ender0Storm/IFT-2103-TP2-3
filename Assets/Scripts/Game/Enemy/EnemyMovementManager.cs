using System.Collections.Generic;
using Game.pathFinding;
using Game.PlayerInformation;
using UnityEngine;

namespace Game.enemy
{
    public class EnemyMovementManager : MonoBehaviour
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
        public int damage;
        private float _nextPositionY;

        private GameObject health;

        private Player player;
        

        public void Start()
        {
            town = GameObject.Find("Town");
            player = town.GetComponent<Player>();
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
                transform.position = new Vector2(_nextPositionX, _nextPositionY);
                if (indexPosition == _path.Count - 1)
                {
                    player.LoseHealthPoints(20);
                    Destroy(gameObject);
                    return;
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
