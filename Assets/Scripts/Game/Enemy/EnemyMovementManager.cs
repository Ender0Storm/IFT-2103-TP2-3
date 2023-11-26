using System.Collections.Generic;
using Game.pathFinding;
using Game.playerInformation;
using UnityEngine;

namespace Game.enemy
{
    public class EnemyMovementManager : MonoBehaviour
    {
        private const float PRECISION = 0.05f;

        private List<Tile> _path;
        private Tile _nextTile;
        [SerializeField]
        private int _indexPosition;
        private Vector2 _direction;
        private Vector2 _nextPosition;

        private Enemy _enemyScript;
        private Player _player;
        

        public void Start()
        {
            _enemyScript = GetComponent<Enemy>();
            _player = GameObject.Find("Player Profile").GetComponent<Player>();
            _path = _enemyScript.path;
        }

        public void Update()
        {
            SetDirection();
            transform.position = (Vector2)transform.position + _direction * Time.deltaTime;

            if (transform.position.x >= _nextPosition.x - PRECISION
                && transform.position.y >= _nextPosition.y - PRECISION
                && transform.position.x <= _nextPosition.x + PRECISION
                && transform.position.y <= _nextPosition.y + PRECISION)
            {
                if (_indexPosition == _path.Count - 1)
                {
                    Destroy(gameObject);
                    _player.LoseHealthPoints(_enemyScript.damage);
                    return;
                }
                _indexPosition++;
            }
        }

        private void SetDirection()
        {
            _nextTile = _path[_indexPosition];
            _nextPosition = _nextTile.GetCenter();
            _direction = (_nextPosition - (Vector2)transform.position).normalized * _enemyScript.speed;
        }

        public float GetProgress()
        {
            return _indexPosition + 1 - Mathf.Clamp01(((Vector2)transform.position - _nextPosition).sqrMagnitude);
        }
    }
}
