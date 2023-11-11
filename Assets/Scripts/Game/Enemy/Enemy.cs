using Unity.VisualScripting;
using UnityEngine;

namespace Game.enemy
{
    public class Enemy : MonoBehaviour
    {

        private Vector2 spawnPosition;
        private float damage;

        public Enemy(Vector2 spawnPosition, float speed, float damage)
        {
            this.spawnPosition = spawnPosition;
            this.damage = damage;
        }
        private GameObject prefab;

        public GameObject CreateEnemy()
        {
            prefab = Resources.Load("Prefabs/Enemy") as GameObject;
            GameObject enemy = Instantiate(prefab, spawnPosition, Quaternion.identity);
            EnemyMovementManager script = enemy.GetComponent<EnemyMovementManager>();
            script.damage = 20;
            script.indexPosition = 0;
            
            Characteristics script2 = enemy.GetComponent<Characteristics>();
            script2.damage = 20;
            return enemy;
        }
    }
}
