using Game.Menu;
using Game.PlayerInformation;
using UnityEngine;

namespace Game
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
        private HealthBar _healthBar;
        
        public string difficulty;

        [SerializeField]
        private PauseMenu _pauseMenu;
        [SerializeField]
        private WaveManager _waveManager;

        public void Start()
        {
            difficulty = Navigation.difficulty;

            _maxHealth = _maxHealth * (difficulty == "hard" ? _hardHealthRatio : 1);
            _currentHealth = _maxHealth;
        }

        public void Update()
        {
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
                _currentHealth -= health;
            }
        }
    }
}