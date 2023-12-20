﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.pathFinding
{
    public class PathFinding : MonoBehaviour
    {
        private Tile _start;
        private Tile _finish;
        public Transform _startPosition;
        public Transform _finishPosition;
        private List<Tile> _activeTiles;
        private List<Tile> _visitedTiles;
        private List<Tile> _foundPath;
        public AccessiblePositionsFinder _accessiblePositionsFinder;
        [SerializeField]
        public LayerMask _layerObstacles;
        
        public void Start()
        {
            _start = new Tile(_startPosition.position);
            _finish = new Tile(_finishPosition.position);
            
            _accessiblePositionsFinder = new AccessiblePositionsFinder();
        }

        public List<Tile> FindPath()
        {
            _foundPath = new List<Tile>();
            _activeTiles = new List<Tile>();
            _visitedTiles = new List<Tile>();
            
            _activeTiles.Add(_start);
            
            _accessiblePositionsFinder.SetupTiles(_layerObstacles);
            
            while (_activeTiles.Any())
            {
                var checkTile = _activeTiles.OrderBy(x => x.CostDistance).First();

                if (checkTile.X == _finish.X && checkTile.Y == _finish.Y)
                {
                    var tile = checkTile;
                    while (tile.X != _start.X || tile.Y != _start.Y)
                    {
                        _foundPath.Insert(0, tile);
                        tile = tile.Parent;
                    }
                    return _foundPath;
                }

                _visitedTiles.Add(checkTile);
                _activeTiles.Remove(checkTile);
                var walkableTiles = _accessiblePositionsFinder.GetAccessibleTiles(checkTile, _finish);

                foreach (var walkableTile in walkableTiles)
                {
                    if (_visitedTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                        continue;
                    if (_activeTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                    {
                        var existingTile = _activeTiles.First(x => x.X == walkableTile.X && x.Y == walkableTile.Y);
                        if (existingTile.CostDistance > walkableTile.CostDistance)
                        {
                            _activeTiles.Remove(existingTile);
                            _activeTiles.Add(walkableTile);
                        }
                    }
                    else
                    {
                        _activeTiles.Add(walkableTile);
                    }
                }
            }
            Debug.Log("Path not found");
            return null;
        }

        public void SetMPPositions(Transform board)
        {
            Transform boardTransform = GameObject.Find($"BoardP{Globals.PlayerID}").transform;
            _startPosition = boardTransform.Find("Spawn Portal");
            _finishPosition = boardTransform.Find("Town");

            _start = new Tile(_startPosition.position);
            _finish = new Tile(_finishPosition.position);
        }
    }
}
