using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ElementsManager
    {
        public List<BoxCollider2D> Obstacles;

        public ElementsManager()
        {
            Obstacles = new List<BoxCollider2D>();
            AddWallsToObstacles();
        }
    
        private void AddWallsToObstacles()
        {   
            var walls = GameObject.Find("Walls");
            foreach (Transform child in walls.transform)
            {
                GameObject obstacle = child.gameObject;
                Obstacles.Add(obstacle.GetComponent<BoxCollider2D>());
            }
        }
    }
}
