using UnityEngine;
using UnityEngine.UI;

namespace Game.enemy
{
    public class EnemyLifeBar : MonoBehaviour
    {
        public Slider slider;
        
        public void SetMaxHealth(float health)
        {
            slider.maxValue = health;
            slider.value = health;
        }

        public void SetHealth(float health)
        {
            slider.value = health;
        }
    }
}