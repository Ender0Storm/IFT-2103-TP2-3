using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : Projectile
{
    private Transform trackedTarget;

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
            trackedTarget.GetComponent<Enemy>().DealDamage(projectileDamage);
            Destroy(gameObject);
        }
    }

    public void Setup(GameObject target, int damage)
    {
        trackedTarget = target.transform;
        this.target = trackedTarget.position;
        projectileDamage = damage;
    }
}
