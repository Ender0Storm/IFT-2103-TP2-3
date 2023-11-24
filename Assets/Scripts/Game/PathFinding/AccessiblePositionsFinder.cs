using System;
using  System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.pathFinding
{
    public class AccessiblePositionsFinder
    {
        private SpriteRenderer _spriteRenderer;
        private List<Tile> _accessibleTiles;
        private List<Tile> _allTiles;
        private const int TileRange = 1;
        private LayerMask _obstacles;

        public List<Tile> GetAccessibleTiles(Tile currentTile, Tile targetTile)
        {
            _accessibleTiles = new List<Tile>();
            
            CardinalPositions(currentTile);
            DiagonalPositions(currentTile);
            _accessibleTiles.ForEach(tile => tile.SetDistance(targetTile.X, targetTile.Y));
            return _accessibleTiles;
        }

        private void CardinalPositions(Tile currentTile)
        {
            var cardinalTiles = new List<Tile>
            {
                new Tile(currentTile.X, currentTile.Y + TileRange) { Parent = currentTile, Cost = currentTile.Cost + TileRange }, //North
                new Tile(currentTile.X, currentTile.Y - TileRange) { Parent = currentTile, Cost = currentTile.Cost + TileRange }, //South
                new Tile(currentTile.X + TileRange, currentTile.Y) { Parent = currentTile, Cost = currentTile.Cost + TileRange }, //East
                new Tile(currentTile.X - TileRange, currentTile.Y) { Parent = currentTile, Cost = currentTile.Cost + TileRange }, //West
            };

            foreach (var tile in cardinalTiles)
            {
                if (_allTiles.Any(x => x.X == tile.X && x.Y == tile.Y))
                {
                    _accessibleTiles.Add(tile);
                }
            }
        }

        private void DiagonalPositions(Tile currentTile)
        {
            var diagonalTiles = new List<Tile>();
            var diagonalCost = currentTile.Cost + TileRange * (float)Math.Sqrt(2);

            if (_accessibleTiles.Any(x => x.X == currentTile.X + TileRange && x.Y == currentTile.Y) && //N
                _accessibleTiles.Any(x => x.X == currentTile.X && x.Y == currentTile.Y + TileRange)) //E
            {
                diagonalTiles.Add(new Tile(currentTile.X + TileRange, currentTile.Y + TileRange)
                    { Parent = currentTile, Cost = diagonalCost }); //N-E
            }
            if (_accessibleTiles.Any(x => x.X == currentTile.X + TileRange && x.Y == currentTile.Y) && //N
                _accessibleTiles.Any(x => x.X == currentTile.X && x.Y == currentTile.Y - TileRange)) //W
            {
                diagonalTiles.Add(new Tile(currentTile.X + TileRange, currentTile.Y - TileRange)
                    { Parent = currentTile, Cost = diagonalCost }); //N-W
            }
            if (_accessibleTiles.Any(x => x.X == currentTile.X - TileRange && x.Y == currentTile.Y) && //S
                _accessibleTiles.Any(x => x.X == currentTile.X && x.Y == currentTile.Y + TileRange)) //E
            {
                diagonalTiles.Add(new Tile(currentTile.X - TileRange, currentTile.Y + TileRange)
                    { Parent = currentTile, Cost = diagonalCost }); //S-E
            }
            if (_accessibleTiles.Any(x => x.X == currentTile.X - TileRange && x.Y == currentTile.Y) && //S
                _accessibleTiles.Any(x => x.X == currentTile.X && x.Y == currentTile.Y - TileRange)) //W
            {
                diagonalTiles.Add(new Tile(currentTile.X - TileRange, currentTile.Y - TileRange)
                    { Parent = currentTile, Cost = diagonalCost }); //S-W
            }
            
            foreach (var tile in diagonalTiles)
            {
                if (_allTiles.Any(x => x.X == tile.X && x.Y == tile.Y))
                {
                    _accessibleTiles.Add(tile);
                }
            }
        }

        public void SetupTiles(LayerMask layerObstacles)
        {
            var ground = GameObject.Find("Ground");
            SpriteRenderer groundLimits = ground.GetComponent<SpriteRenderer>();
            _allTiles = new List<Tile>();
            for (int i = Mathf.CeilToInt(groundLimits.bounds.min.x); i <= groundLimits.bounds.max.x; i++)
            {
                for (int j = Mathf.CeilToInt(groundLimits.bounds.min.y); j <= groundLimits.bounds.max.y; j++)
                {
                    Tile tile = new Tile(i, j);
                    if (!Physics2D.OverlapPoint(tile.GetCenter(), layerObstacles))
                    {
                        _allTiles.Add(tile);
                    }
                }
            }
        }
    }
}