using System.Collections.Generic;
using Game.pathFinding;
using UnityEngine;

namespace Game.enemy
{
    public class EnemyMovementManager : MonoBehaviour
    {
        private const float PRECISION = 0.05f;

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
        }

        public void Update()
        {
            SetDirection();
            transform.position = new Vector2(transform.position.x + _direction.x * Time.deltaTime, transform.position.y + _direction.y * Time.deltaTime);

            if (transform.position.x >= _nextPosition.x - PRECISION
                && transform.position.y >= _nextPosition.y - PRECISION
                && transform.position.x <= _nextPosition.x + PRECISION
                && transform.position.y <= _nextPosition.y + PRECISION)
            {
                if (indexPosition == _path.Count - 1)
                {
                    player.LoseHealthPoints(enemyScript.damage);
                    Destroy(gameObject);
                    return;
                }
                indexPosition++;
            }
        }

        private void SetDirection()
        {
            _nextTile = _path[indexPosition];
            _nextPosition = _nextTile.GetCenter();
            _direction = (_nextPosition - (Vector2)transform.position).normalized * enemyScript.speed;
        }
    }
}
