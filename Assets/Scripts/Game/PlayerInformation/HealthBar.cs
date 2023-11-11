using UnityEngine;
using UnityEngine.UI;

namespace Game.PlayerInformation
{
    public class HealthBar : MonoBehaviour
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
            Debug.Log(slider.value);
        }
    }
}