using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private float cooldownRemaining;

    // Start is called before the first frame update
    void Start()
    {
        cooldownRemaining = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownRemaining > 0)
        {
            cooldownRemaining = Mathf.Max(cooldownRemaining - Time.deltaTime, 0);
        }

        if (cooldownRemaining == 0)
        {
            Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyMask);
        }
    }
}
