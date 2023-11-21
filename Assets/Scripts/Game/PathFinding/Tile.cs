using System;

namespace Game
{
    public class Tile
    {
        public float X;
        public float Y;
        public float Cost;
        public float Distance;
        public float CostDistance => Cost + Distance;
        public Tile Parent;
    
        public void SetDistance(float targetX, float targetY)
        {
            Distance = Math.Abs(targetX - X) + Math.Abs(targetY - Y);
        }
    }
}
