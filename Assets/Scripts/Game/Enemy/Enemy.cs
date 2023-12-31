using System.Collections.Generic;
using Game.pathFinding;
using Game.playerInformation;
using Unity.Netcode;
using UnityEngine;

namespace Game.enemy
{
    public class Enemy : NetworkBehaviour
    {
        public int damage;
        public int health;
        public float speed;
        public int currencyDrop;
        public List<Tile> path;
        [SerializeField] private Animator _enemyAnimator;
        [SerializeField] private EnemyLifeBar _enemyLifeBar;

        public void Start()
        {
            _enemyLifeBar.SetMaxHealth(health);
        }

        public void DealDamage(int damage, BuildController damageDealer)
        {
            health -= damage;
            _enemyLifeBar.SetHealth(health);
        
            if (health <= 0)
            {
                if (damageDealer != null) damageDealer.AddCurrency(currencyDrop);
                Destroy(gameObject);
            }
            _enemyAnimator.SetTrigger("damages");
        }
    }
}

