using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonProjectile : Projectile
{
    [SerializeField]
    private float splashRange;
    [SerializeField]
    private LayerMask enemyMask;

    // Update is called once per frame
    void Update()
    {
        if (trackedTarget != null)
        {
            target = trackedTarget.position;
        }

        Vector3 direction = target - (Vector2)transform.position;
        transform.position += direction * projectileSpeed * Time.deltaTime;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        if (((Vector2)transform.position - target).sqrMagnitude < projectileSize * projectileSize)
        {
            Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(transform.position, splashRange, enemyMask);

            foreach (Collider2D enemy in enemiesHit)
            {
                enemy.gameObject.GetComponent<Enemy>().DealDamage(projectileDamage, owner);
            }

            Destroy(gameObject);
        }
    }
}
