using UnityEngine;
using UnityEngine.UI;

namespace Game.enemy
{
    public class EnemyLifeBar : MonoBehaviour
    {
        public Slider slider;
        public ParticleSystem particles;
        public void SetMaxHealth(float health)
        {
            slider.maxValue = health;
            slider.value = health;
        }

        public void SetHealth(float health)
        {
            if(health < slider.value)
            {
                particles.Play();
            }
            slider.value = health;
            if(health <= 0)
            {
                ParticleSystem deathParticles = Instantiate(particles);
                TimedDestruction timedDestruction = deathParticles.gameObject.AddComponent<TimedDestruction>();
                deathParticles.transform.position = particles.transform.position;
                deathParticles.emission.SetBurst(0, new ParticleSystem.Burst(0, 50));
                deathParticles.Play();
                timedDestruction.DeleteIn(deathParticles.main.startLifetime.constant + 1);
            }
        }
    }
}