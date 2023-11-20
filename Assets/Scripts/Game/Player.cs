using Game.Menu;
using Game.PlayerInformation;
using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour
    {
        public float maxHealth;

        public float currentHealth;

        public HealthBar healthBar;
        public string levelDifficulty;

        public void Start()
        {
            healthBar.SetMaxHealth(maxHealth);
            levelDifficulty = Navigation.SelectedDifficulty;
            Debug.Log(levelDifficulty);
        }

        public void LoseHealthPoints(float health)
        {
            if (currentHealth - health < 0)
            {
                currentHealth = 0;
                Debug.Log("GAME OVER");
            }
            else
            {
                currentHealth -= health;
            }
            healthBar.SetHealth(currentHealth);
        }

        public void WinHealthPoints(float health)
        {
            if (currentHealth + health > maxHealth)
            {
                currentHealth = maxHealth;
            }
            else
            {
                currentHealth += health;
            }
            healthBar.SetHealth(currentHealth);
        }
    }
}