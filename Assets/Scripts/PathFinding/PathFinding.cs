using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace pathFinding
{
    public class PathFinding
    {
        private readonly Tile _start;
        private readonly Tile _finish;
        private List<Tile> _activeTiles;
        private List<Tile> _visitedTiles;
        private List<Tile> _foundPath;
        private AccessiblePositionsFinder _accessiblePositionsFinder;

        public PathFinding(Tile start, Tile finish)
        {
            _start = start;
            _finish = finish;
            Start();
        }
        
        public void Start()
        {
            _accessiblePositionsFinder = new AccessiblePositionsFinder();
            
            _foundPath = new List<Tile>();
            _activeTiles = new List<Tile>();
            _visitedTiles = new List<Tile>();
            
            _activeTiles.Add(_start);
        }

        public List<Tile> FindPath()
        {
            while (_activeTiles.Any())
            {
                var checkTile = _activeTiles.OrderBy(x => x.CostDistance).First();

                if (checkTile.X == _finish.X && checkTile.Y == _finish.Y)
                {
                    var tile = checkTile;
                    Debug.Log("Retracing steps backwards...");
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
                        if (existingTile.CostDistance > checkTile.CostDistance)
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
            Debug.Log("No Path Found!");
            return null;
        }
    }
}
