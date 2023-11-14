using Game.enemy;
using UnityEngine;

namespace Game
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject enemyPrefab;
        [SerializeField]
        private Transform portalTransform;

        public void Update()
        {
            if (Input.GetKeyDown("space"))
            {
                GameObject enemy = Instantiate(enemyPrefab, portalTransform.position, Quaternion.identity);
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                enemyScript.damage = 20;
                enemyScript.health = 40;
            }
        }
    }
}