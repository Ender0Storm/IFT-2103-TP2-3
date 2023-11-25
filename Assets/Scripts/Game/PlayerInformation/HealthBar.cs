using UnityEngine;
using UnityEngine.UI;

namespace Game.PlayerInformation
{
    public class HealthBar : MonoBehaviour
    {
        public Slider slider;
        public Text hp;

        public void SetMaxHealth(float health)
        {
            slider.maxValue = health;
            slider.value = health;
            hp.text = "100 / 100";

        }

        public void SetHealth(float health)
        {
            slider.value = health;
            hp.text = health + " / 100";
        }
    }
}