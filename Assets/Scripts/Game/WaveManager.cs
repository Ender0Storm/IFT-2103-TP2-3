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
        private GameObject enemyPrefab;
        [SerializeField]
        private Transform portalTransform;

        private List<GameObject> enemiesAlive;

        private bool finishedSummoning;


        public void Start()
        {
            enemiesAlive = new List<GameObject>();
        }

        public void Update()
        {
            if (Input.GetKeyDown("space"))
            {
                StartCoroutine(SummonWave(enemyPrefab, 5, 1.5f));
            }
        }

        private void CleanList()
        {
            enemiesAlive.RemoveAll(enemy => enemy == null);
        }

        private IEnumerator SummonWave(GameObject waveEnemyPrefab, int enemyCount, float enemySpawnInterval)
        {
            finishedSummoning = false;

            // TODO: Compute pathfinding here and give to all enemies

            for (int i = 0; i < enemyCount; i++)
            {
                GameObject enemy = Instantiate(waveEnemyPrefab, portalTransform.position, Quaternion.identity);
                enemiesAlive.Add(enemy);
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                enemyScript.damage = 20;
                enemyScript.health = 40;

                yield return new WaitForSeconds(enemySpawnInterval);
            }

            finishedSummoning = true;
        }

        public bool CanBuild()
        {
            CleanList();

            return finishedSummoning && enemiesAlive.Count == 0;
        }
    }
}