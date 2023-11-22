using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public abstract class Projectile : MonoBehaviour
{
    protected Transform trackedTarget;
    protected Vector2 target;
    protected BuildController owner;

    protected int projectileDamage;
    [SerializeField]
    protected float projectileSpeed;
    [SerializeField]
    protected float projectileSize;

    public void Setup(GameObject target, BuildController owner, int damage)
    {
        trackedTarget = target.transform;
        this.target = trackedTarget.position;
        this.owner = owner;
        projectileDamage = damage;
    }
}
