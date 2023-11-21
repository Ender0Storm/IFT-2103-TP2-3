using System;
using  System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.pathFinding
{
    public class AccessiblePositionsFinder
    {
        private SpriteRenderer _spriteRenderer;
        private CollisionManager _collisionManager;
        private List<Tile> _accessibleTiles;
        private const float TileRange = 1f;

        public List<Tile> GetAccessibleTiles(Tile currentTile, Tile targetTile)
        {
            _spriteRenderer = GetMapLimits();
            _collisionManager = new CollisionManager();
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
                new Tile { X = currentTile.X, Y = currentTile.Y + TileRange, Parent = currentTile, Cost = currentTile.Cost + TileRange }, //North
                new Tile { X = currentTile.X, Y = currentTile.Y - TileRange, Parent = currentTile, Cost = currentTile.Cost + TileRange }, //South
                new Tile { X = currentTile.X + TileRange, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + TileRange }, //East
                new Tile { X = currentTile.X - TileRange, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + TileRange }, //West
            };
            
            foreach(var tile in cardinalTiles)
            {
                if (!_collisionManager.IsSquareColliding(tile, TileRange) &&
                    tile.X > _spriteRenderer.bounds.min.x &&
                    tile.X < _spriteRenderer.bounds.max.x &&
                    tile.Y > _spriteRenderer.bounds.min.y &&
                    tile.Y < _spriteRenderer.bounds.max.y)
                {
                    _accessibleTiles.Add(tile);
                }
            }
        }

        private void DiagonalPositions(Tile currentTile)
        {
            var diagonalTiles = new List<Tile>();

            if (_accessibleTiles.Any(x => x.X == currentTile.X + TileRange && x.Y == currentTile.Y) && //N
                _accessibleTiles.Any(x => x.X == currentTile.X && x.Y == currentTile.Y + TileRange)) //E
            {
                diagonalTiles.Add(new Tile
                    { X = currentTile.X + TileRange, Y = currentTile.Y + TileRange, Parent = currentTile, Cost = currentTile.Cost + TileRange*(float)Math.Sqrt(2)}); //N-E
            }
            if (_accessibleTiles.Any(x => x.X == currentTile.X + TileRange && x.Y == currentTile.Y) && //N
                _accessibleTiles.Any(x => x.X == currentTile.X && x.Y == currentTile.Y - TileRange)) //W
            {
                diagonalTiles.Add(new Tile
                    { X = currentTile.X + TileRange, Y = currentTile.Y - TileRange, Parent = currentTile, Cost = currentTile.Cost + TileRange*(float)Math.Sqrt(2)}); //N-W
            }
            if (_accessibleTiles.Any(x => x.X == currentTile.X - TileRange && x.Y == currentTile.Y) && //S
                _accessibleTiles.Any(x => x.X == currentTile.X && x.Y == currentTile.Y + TileRange)) //E
            {
                diagonalTiles.Add(new Tile
                    { X = currentTile.X - TileRange, Y = currentTile.Y + TileRange, Parent = currentTile, Cost = currentTile.Cost + TileRange*(float)Math.Sqrt(2)}); //S-E
            }
            if (_accessibleTiles.Any(x => x.X == currentTile.X - TileRange && x.Y == currentTile.Y) && //S
                _accessibleTiles.Any(x => x.X == currentTile.X && x.Y == currentTile.Y - TileRange)) //W
            {
                diagonalTiles.Add(new Tile
                    { X = currentTile.X - TileRange, Y = currentTile.Y - TileRange, Parent = currentTile, Cost = currentTile.Cost + TileRange*(float)Math.Sqrt(2)}); //S-W
            }
            
            foreach(var tile in diagonalTiles)
            {
                if (!_collisionManager.IsSquareColliding(tile, TileRange) &&
                    tile.X > _spriteRenderer.bounds.min.x &&
                    tile.X < _spriteRenderer.bounds.max.x &&
                    tile.Y > _spriteRenderer.bounds.min.y &&
                    tile.Y < _spriteRenderer.bounds.max.y)
                {
                    _accessibleTiles.Add(tile);
                }
            }
        }

        private static SpriteRenderer GetMapLimits()
        {
            var ground = GameObject.Find("Ground");
            return ground.GetComponent<SpriteRenderer>();
        }
    }
}