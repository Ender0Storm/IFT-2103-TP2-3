using UnityEngine;
using UnityEngine.UI;

namespace Game.playerInformation
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Slider _slider;
        [SerializeField]
        private Text _hpText;    
        [SerializeField]
        private ParticleSystem _particles;

        public void SetHealth(float maxHealth, float currentHealth)
        {
            _slider.maxValue = maxHealth;
            _slider.value = currentHealth;
            SetText();
        }

        public void LoseHealth(float nbHP)
        {
            _slider.value = Mathf.Max(_slider.value - nbHP, 0);
            SetText();
            _particles.Play();
        }

        private void SetText()
        {
            _hpText.text = $"{_slider.value} / {_slider.maxValue}";
        }
    }
}