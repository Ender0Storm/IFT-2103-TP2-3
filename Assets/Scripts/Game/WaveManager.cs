using System;
using System.Collections;
using System.Collections.Generic;
using Game.enemy;
using Game.menu;
using Game.pathFinding;
using Game.playerInformation;
using Game.ui;
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

        [SerializeField]
        private GameObject player;

        [Header("Other")]
        [SerializeField]
        protected Transform _portalTransform;
        [SerializeField]
        [Range(0f, 1f)]
        private float _hardCurrencyRatio;

        private List<GameObject> _enemiesAlive;
        private int _waveCount;
        protected PathFinding _pathFinding;
        private ControlsManager _controlsManager;

        private bool _finishedSummoning;
        
        public void Start()
        {
            _controlsManager = player.GetComponent<ControlsManager>();
            _enemiesAlive = new List<GameObject>();
            _finishedSummoning = true;
            _waveCount = 0;
            _pathFinding = gameObject.GetComponent<PathFinding>();
        }

        public void Update()
        {
            if (_controlsManager.IsKeyDown("space") && CanBuild() && !PauseMenu.isPaused)
            {
                StartWave();
            }
        }

        public void StartWave()
        {
            List<Tile> path = _pathFinding.FindPath();
            StartCoroutine(SummonWave(path));
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
            _waveCount += 1;

            for (int i = 0; i < _waves[modWave].enemyCount; i++)
            {
                GameObject enemy = Instantiate(_waves[modWave].enemyPrefab, _portalTransform.position, Quaternion.identity);
                _enemiesAlive.Add(enemy);
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                enemyScript.health = Mathf.FloorToInt(enemyScript.health * Mathf.Pow(strengthMod, waveStrength));
                enemyScript.currencyDrop = Mathf.CeilToInt(enemyScript.currencyDrop * (Navigation.difficulty == "hard" ? _hardCurrencyRatio : 1) * (waveStrength + 1));
                enemyScript.path = path;

                yield return new WaitForSeconds(_waves[modWave].enemyInterval);
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
