using Game.enemy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _enemyPrefab;
        [SerializeField]
        private Transform _portalTransform;

        private List<GameObject> _enemiesAlive;
        private int _waveCount;

        private bool _finishedSummoning;


        public void Start()
        {
            _enemiesAlive = new List<GameObject>();
            _finishedSummoning = true;
            _waveCount = 0;
        }

        public void Update()
        {
            if (Input.GetKeyDown("space") && _finishedSummoning && !PauseMenu.isPaused)
            {
                StartCoroutine(SummonWave(_enemyPrefab, 1, 1.5f));
            }
        }

        private void CleanList()
        {
            _enemiesAlive.RemoveAll(enemy => enemy == null);
        }

        private IEnumerator SummonWave(GameObject waveEnemyPrefab, int enemyCount, float enemySpawnInterval)
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