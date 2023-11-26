using Game.enemy;
using UnityEngine;

namespace Game.towers
{
    public class SimpleTower : Tower
    {
        [SerializeField]
        private int attackDamage;
        [SerializeField]
        private float attackCooldown;
        [SerializeField]
        private float attackRange;
    
        [SerializeField]
        private LayerMask enemyMask;
        [SerializeField]
        private GameObject projectilePrefab;
    
        private float cooldownRemaining;
    
        // Start is called before the first frame update
        void Start()
        {
            cooldownRemaining = 0;
        }
    
        // Update is called once per frame
        void Update()
        {
            Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyMask);
            if (cooldownRemaining > 0)
            {
                cooldownRemaining = Mathf.Max(cooldownRemaining - Time.deltaTime, 0);
            }
    
            if (cooldownRemaining == 0 && enemiesInRange.Length > 0)
            {
                Fire(GetFarthestEnemy(enemiesInRange));
                cooldownRemaining = attackCooldown;
            }
        }
    
        private GameObject GetFarthestEnemy(Collider2D[] enemies)
        {
            if (enemies.Length == 0) { return null; }
    
            GameObject farthestEnemy = enemies[0].gameObject;
            float farthestDistance = farthestEnemy.GetComponent<EnemyMovementManager>().GetProgress();
            for (int i = 1; i < enemies.Length; i++)
            {
                GameObject newEnemy = enemies[i].gameObject;
                float newDistance = newEnemy.GetComponent<EnemyMovementManager>().GetProgress();
    
                if (newDistance > farthestDistance)
                {
                    farthestEnemy = newEnemy;
                    farthestDistance = newDistance;
                }
            }
    
            return farthestEnemy;
        }
    
        private void Fire(GameObject target)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.Setup(target, owner, attackDamage);
        }
    }
}

