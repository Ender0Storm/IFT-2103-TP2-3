using Game.enemy;
using UnityEngine;

namespace Game
{
    public class WaveManager : MonoBehaviour
    {
        public GameObject prefab;
        public void Update()
        {
            if (Input.GetKeyDown("space"))
            {
                //Enemy enemyFactory = this.AddComponent<Enemy>();
                Enemy enemy = new Enemy(new Vector2(-7.5f, -3.5f), 90f, 20f);
                enemy.CreateEnemy();
            }
        }
    }
}