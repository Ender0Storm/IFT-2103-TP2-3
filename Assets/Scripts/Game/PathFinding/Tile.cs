using System;
using UnityEngine;

namespace Game.pathFinding
{
    public class Tile
    {
        public int X;
        public int Y;
        public float Cost;
        public float Distance;
        public float CostDistance => Cost + Distance;
        public Tile Parent;

        public Tile(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Tile(Vector2 vector)
        {
            X = Mathf.FloorToInt(vector.x);
            Y = Mathf.FloorToInt(vector.y);
        }
    
        public void SetDistance(float targetX, float targetY)
        {
            Distance = Math.Abs(targetX - X) + Math.Abs(targetY - Y);
        }

        public Vector2 GetCenter()
        {
            return new Vector2(X + 0.5f, Y + 0.5f);
        }

        public Vector2Int getPosition()
        {
            return new Vector2Int(X, Y);
        }
    }
}
