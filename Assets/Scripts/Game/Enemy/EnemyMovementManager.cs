using System.Collections.Generic;
using Game.pathFinding;
using UnityEngine;

namespace Game.enemy
{
    public class EnemyMovementManager : MonoBehaviour
    {
        private List<Tile> _path;
        private PathFinding _pathFinding;
        private Tile _nextTile;
        [SerializeField]
        private int indexPosition;
        private Vector2 _direction;
        private Vector2 _nextPosition;

        private Enemy enemyScript;
        private Player player;
        

        public void Start()
        {
            enemyScript = GetComponent<Enemy>();
            player = GameObject.Find("Town").GetComponent<Player>();
            enemyScript.speed = player.difficulty == "hard" ? 2 : 6;
            _path = enemyScript.path;
            //GetPath();
            SetDirection();
        }

        public void Update()
        {
            transform.position = new Vector2(transform.position.x + _direction.x * Time.deltaTime, transform.position.y + _direction.y * Time.deltaTime);

            if (transform.position.x >= _nextPosition.x - 0.2 
                && transform.position.y >= _nextPosition.y - 0.2
                && transform.position.x <= _nextPosition.x + 0.2
                && transform.position.y <= _nextPosition.y + 0.2)
            {
                transform.position = new Vector2(_nextPosition.x, _nextPosition.y);
                if (indexPosition == _path.Count - 1)
                {
                    player.LoseHealthPoints(enemyScript.damage);
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
                X = (int)player.transform.position.x,
                Y = (int)player.transform.position.y,
            };
            return finish;
        }

        private void SetDirection()
        {
            _nextTile = _path[indexPosition];
            _nextPosition.x = _nextTile.X + 0.5f;
            _nextPosition.y = _nextTile.Y + 0.5f;
            _direction = (_nextPosition - (Vector2)transform.position).normalized * enemyScript.speed;
        }
    }
}
