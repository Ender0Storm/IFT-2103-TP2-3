using Game.menu;
using Game.ui;
using UnityEngine;

namespace Game.playerInformation
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private float _maxHealth;
        private float _currentHealth;

        [SerializeField]
        [Range(0f, 1f)]
        private float _hardHealthRatio;

        [SerializeField]
        private GameObject _particlesPrefab;

        [SerializeField]
        private HealthBar _healthBar;

        [SerializeField]
        private PauseMenu _pauseMenu;
        [SerializeField]
        private WaveManager _waveManager;

        public void Start()
        {
            Globals.IsMultiplayer = !string.IsNullOrEmpty(Navigation.joinIP);

            _maxHealth = _maxHealth * (Navigation.difficulty == "hard" ? _hardHealthRatio : 1);
            _currentHealth = _maxHealth;
            _healthBar.SetHealth(_maxHealth, _currentHealth);
        }
        public void LoseHealthPoints(float health)
        {
            if (_currentHealth - health <= 0)
            {
                _currentHealth = 0;
                _pauseMenu.EndGame(_waveManager.GetWave());
            }
            else
            {
                SoundManager.PlaySound(SoundManager.Sound.LoseLife);
                GameObject particles = Instantiate(_particlesPrefab);
                TimedDestruction timedDestruction = particles.AddComponent<TimedDestruction>();
                Vector2Int townPosition = _waveManager.GetMap().GetTownPosition();
                particles.transform.position = new Vector3(townPosition.x + _waveManager.GetMap().transform.position.x + 0.5f, townPosition.y + _waveManager.GetMap().transform.position.y + 0.5f);
                ParticleSystem PS = particles.GetComponent<ParticleSystem>();
                PS.Play();
                timedDestruction.DeleteIn(PS.main.startLifetime.constant + 1);

                _currentHealth -= health;
            }
            _healthBar.LoseHealth(health);
        }
    }
}