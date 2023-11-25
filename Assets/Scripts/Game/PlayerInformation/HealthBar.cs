﻿using UnityEngine;
using UnityEngine.UI;

namespace Game.PlayerInformation
{
    public class HealthBar : MonoBehaviour
    {
        public Slider slider;
        public GameObject hp;
        private Text _hpText;

        void Start()
        {
            _hpText = hp.GetComponent<Text>();
        }
        public void SetMaxHealth(float health)
        {
            slider.maxValue = health;
            slider.value = health;
            _hpText.text = "100 / " + health;

        }

        public void SetHealth(float health)
        {
            slider.value = health;
            _hpText.text = health + " / 100";
        }
    }
}