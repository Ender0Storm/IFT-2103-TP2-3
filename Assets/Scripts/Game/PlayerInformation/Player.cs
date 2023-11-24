﻿using Game.Menu;
using Game.PlayerInformation;
using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private float _maxHealth;

        [SerializeField]
        private float _currentHealth;

        [SerializeField]
        private HealthBar _healthBar;
        public string difficulty;

        [SerializeField]
        private PauseMenu _pauseMenu;
        [SerializeField]
        private WaveManager _waveManager;

        public void Start()
        {
            _healthBar.SetMaxHealth(_maxHealth);
            difficulty = Navigation.Difficulty;
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
            _healthBar.SetHealth(_currentHealth);
        }

        public void WinHealthPoints(float health)
        {
            if (_currentHealth + health > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
            else
            {
                _currentHealth += health;
            }
            _healthBar.SetHealth(_currentHealth);
        }
    }
}