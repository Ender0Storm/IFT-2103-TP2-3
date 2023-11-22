using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Game.pathFinding;
using UnityEngine;

namespace Game
{
    [Serializable]
    public struct WaveInfo
    {
        public GameObject enemyPrefab;
        public int enemyCount;
        public float enemyInterval;
    }

    public class WaveManager : MonoBehaviour
    {
        [Header("Wave info")]
        [SerializeField]
        private List<WaveInfo> _waves;
        [SerializeField]
        private float strengthMod;

        [Header("Other")]
        [SerializeField]
        private Transform _portalTransform;
        [SerializeField]
        private float hardmodeCurrencyFactor;

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
                StartWave();
            }
        }

        public void StartWave()
        {
            StartCoroutine(GetPath(path =>
                {
                    StartCoroutine(SummonWave(path));
                }));
        }

        private void CleanList()
        {
            _enemiesAlive.RemoveAll(enemy => enemy == null);
        }

        private IEnumerator SummonWave(List<Tile> path)
        {
            _finishedSummoning = false;

            int modWave = _waveCount % _waves.Count;
            int waveStrength = _waveCount / _waves.Count;

            for (int i = 0; i < _waves[modWave].enemyCount; i++)
            {
                GameObject enemy = Instantiate(_waves[modWave].enemyPrefab, _portalTransform.position, Quaternion.identity);
                _enemiesAlive.Add(enemy);
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                Player player = GameObject.Find("Town").GetComponent<Player>();
                enemyScript.health = Mathf.FloorToInt(enemyScript.health * Mathf.Pow(strengthMod, waveStrength));
                enemyScript.currencyDrop = Mathf.CeilToInt(enemyScript.currencyDrop * (player.difficulty == "hard" ? hardmodeCurrencyFactor : 1) * (waveStrength + 1));
                enemyScript.path = path;

                yield return new WaitForSeconds(_waves[modWave].enemyInterval);
            }

            _waveCount += 1;
            _finishedSummoning = true;
        }

        private IEnumerator GetPath(Action<List<Tile>> callback)
        {
            PathFinding pathFinding = new PathFinding(start, finish);
            List<Tile> path = pathFinding.FindPath();
            
            callback(path);

            yield return new WaitForSeconds(2);;
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