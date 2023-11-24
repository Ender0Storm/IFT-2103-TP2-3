using System;
using UnityEngine;

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

        public Vector2 GetCenter()
        {
            return new Vector2(X + 0.5f, Y + 0.5f);
        }
    }
}
