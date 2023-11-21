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
            _path = enemyScript.path;
            SetDirection();
        }

        public void Update()
        {
            transform.position = new Vector2(transform.position.x + _direction.x * Time.deltaTime, transform.position.y + _direction.y * Time.deltaTime);
            if (transform.position.x >= _nextPosition.x - 0.1 
                && transform.position.y >= _nextPosition.y - 0.1
                && transform.position.x <= _nextPosition.x + 0.1
                && transform.position.y <= _nextPosition.y + 0.1)
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

        private void SetDirection()
        {
            _nextTile = _path[indexPosition];
            _nextPosition.x = _nextTile.X + 0.5f;
            _nextPosition.y = _nextTile.Y + 0.5f;
            _direction = (_nextPosition - (Vector2)transform.position).normalized * enemyScript.speed;
        }
    }
}
