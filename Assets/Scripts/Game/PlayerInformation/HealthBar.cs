using UnityEngine;
using UnityEngine.UI;

namespace Game.PlayerInformation
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Slider _slider;
        [SerializeField]
        private Text _hpText;

        public void SetHealth(float maxHealth, float currentHealth)
        {
            _slider.maxValue = maxHealth;
            _slider.value = currentHealth;
            _hpText.text = $"{currentHealth} / {maxHealth}";
        }
    }
}