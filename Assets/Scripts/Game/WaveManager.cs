using System.Collections;
using System.Collections.Generic;
using Game.pathFinding;
using UnityEngine;

namespace Game
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _enemyPrefab;
        [SerializeField]
        private Transform _portalTransform;

        private Vector2 town;
        private Vector2 portal;
        private Tile start;
        private Tile finish;

        private List<GameObject> _enemiesAlive;
        private int _waveCount;

        private bool _finishedSummoning;


        public void Start()
        {
            _enemiesAlive = new List<GameObject>();
            _finishedSummoning = true;
            _waveCount = 0;
            town = GameObject.Find("Town").transform.position;
            portal = GameObject.Find("Spawn Portal").transform.position;
            start = new Tile
            {
                X = portal.x,
                Y = portal.y
            };
            finish = new Tile
            {
                X = town.x,
                Y = town.y
            };
        }

        public void Update()
        {
            if (Input.GetKeyDown("space") && _finishedSummoning && !PauseMenu.isPaused)
            {
                PathFinding pathFinding = new PathFinding(start, finish);
                List<Tile> path = pathFinding.FindPath();
                StartCoroutine(SummonWave(_enemyPrefab, 1, 1.5f, path));
            }
        }

        private void CleanList()
        {
            _enemiesAlive.RemoveAll(enemy => enemy == null);
        }

        private IEnumerator SummonWave(GameObject waveEnemyPrefab, int enemyCount, float enemySpawnInterval, List<Tile> path)
        {
            _finishedSummoning = false;
            _waveCount += 1;

            // TODO: Compute pathfinding here and give to all enemies

            for (int i = 0; i < enemyCount; i++)
            {
                GameObject enemy = Instantiate(waveEnemyPrefab, _portalTransform.position, Quaternion.identity);
                _enemiesAlive.Add(enemy);
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                enemyScript.damage = 20;
                enemyScript.health = 40;
                enemyScript.path = path;

                yield return new WaitForSeconds(enemySpawnInterval);
            }

            _finishedSummoning = true;
        }

        public bool CanBuild()
        {
            CleanList();

            return _finishedSummoning && _enemiesAlive.Count == 0;
        }

        public int GetWave()
        {
            return _waveCount;
        }
    }
}