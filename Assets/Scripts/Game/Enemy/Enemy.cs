using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage;
    public int health;
    public float speed;
    public int currencyDrop;
    public List<Tile> path;

    public void DealDamage(int damage, BuildController damageDealer)
    {
        health -= damage;
        if (health <= 0)
        {
            damageDealer.AddCurrency(currencyDrop);
            Destroy(gameObject);
        }
    }
}
