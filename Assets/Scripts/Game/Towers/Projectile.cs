using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public abstract class Projectile : MonoBehaviour
{
    protected Vector2 target;

    [SerializeField]
    protected int projectileDamage;
    [SerializeField]
    protected float projectileSpeed;
    [SerializeField]
    protected float projectileSize;
}
