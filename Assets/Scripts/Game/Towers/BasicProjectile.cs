using Game.enemy;
using UnityEngine;

namespace Game.towers
{
    public class BasicProjectile : Projectile
    {
        // Update is called once per frame
        void Update()
        {
            if (trackedTarget == null)
            {
                Destroy(gameObject);
                return;
            }
            target = trackedTarget.position;

            Vector3 direction = target - (Vector2)transform.position;
            transform.position += direction * projectileSpeed * Time.deltaTime;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);

            if (((Vector2)transform.position - target).sqrMagnitude < projectileSize * projectileSize)
            {
                trackedTarget.GetComponent<Enemy>().DealDamage(projectileDamage, owner);
                Destroy(gameObject);
            }
        }
    }
}
