using UnityEngine;

public class CollisionManager
{
    public bool IsSquareColliding(Tile tile, float tileRange)
    {
        var obstacleClass = new ElementsManager();
        var obstacles = obstacleClass.Obstacles;
        foreach (var obstacle in obstacles)
        {
            var x = obstacle.transform.position.x - obstacle.bounds.size.x / 2;
            var y = obstacle.transform.position.y - obstacle.bounds.size.y / 2;
            if (tile.X + 1 > x &&
                tile.X < x + obstacle.bounds.size.x &&
                tile.Y + 1 > y &&
                tile.Y < y + obstacle.bounds.size.y) {
                return true;
            }
        }
        return false;
    }
}