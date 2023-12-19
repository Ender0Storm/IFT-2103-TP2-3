using Game.playerInformation;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapBuilder : MonoBehaviour
{
    private bool pathCreated = false;

    private Vector2Int spawnPosition;
    private Vector2Int townPosition;
    private int mapHeight;
    private int mapWidth;

    private Tilemap tilemap;
    private Grid grid;

    private TileData[,] tiles;
    private Dictionary<string, GameAssets.CustomTile> mapBorderTiles;

    void Start()
    {
        mapBorderTiles = new Dictionary<string, GameAssets.CustomTile>
        {
            {"town", GameAssets.i.tiles[0]},
            {"spawn", GameAssets.i.tiles[1]},
            {"topWall", GameAssets.i.tiles[2]},
            {"rightWall", GameAssets.i.tiles[3]},
            {"bottomWall", GameAssets.i.tiles[4]},
            {"leftWall", GameAssets.i.tiles[5]},
            {"cornerWall", GameAssets.i.tiles[6]},
            {"grass", GameAssets.i.tiles[7]},
        };
        /*var grass = GetCompatibleTiles(mapBorderTiles["grass"].neighbours);
        var rock = GetCompatibleTiles(mapBorderTiles["cornerWall"].neighbours);
        var path = GetCompatibleTiles(GameAssets.i.tiles[8].neighbours);*/
        BuildMap(false, 0);
    }


    public void BuildMap(bool hardmode = false, int seed = 0)
    {
        GenerateMapStats(hardmode, seed);

        GameObject map = new GameObject("Map");
        map.transform.parent = transform;
        map.AddComponent<TilemapRenderer>();

        tilemap = map.GetComponent<Tilemap>();
        grid = map.AddComponent<Grid>();

        transform.localPosition += new Vector3(-mapWidth / 2, -mapHeight / 2, 0);

        GenerateTiles(hardmode);

        generateTilemap();

        tilemap.AddComponent<SetWorldBounds>();
    }
    public bool CanWalk(Vector2Int position)
    {
        return TileExist(position) && tiles[position.x, position.y].canWalk;
    }
    public bool CanBuild(Vector2Int position)
    {
        return TileExist(position) && tiles[position.x, position.y].canBuild;
    }
    public Vector2Int GetSpawnPosition()
    {
        return spawnPosition;
    }
    public Vector2Int GetTownPosition()
    {
        return townPosition;
    }
    public int GetMapHeight()
    {
        return mapHeight;
    }
    public int GetMapWidth()
    {
        return mapWidth;
    }

    private bool IsInbound(Vector2Int position)
    {
        return position.x >= 0 && position.x < mapWidth && position.y >= 0 && position.y < mapHeight;
    }
    private bool TileExist(Vector2Int position)
    {
        return IsInbound(position) && tiles[position.x, position.y] != null;
    }
    private void GenerateMapStats(bool hardmode = false, int seed = 0)
    {
        if (seed != 0)
        {
            Random.InitState(seed);
        }
        mapHeight = Random.Range(20, 30);
        mapWidth = Random.Range(20, 30);

        if (hardmode)
        {
            mapHeight -= Random.Range(mapHeight / 4, mapHeight / 2);
            mapWidth -= Random.Range(mapWidth / 4, mapHeight / 2);
        }
        float minDistanceBetweenSpawnAndVillage = Mathf.Sqrt(mapWidth * mapWidth + mapHeight * mapHeight) / 3; /* 1/3 of the diagonal of the map */

        tiles = new TileData[mapWidth, mapHeight];

        spawnPosition = new Vector2Int(Random.Range(1, (mapWidth - 2) / 2), Random.Range(1, (mapHeight - 2) / 2));
        townPosition = spawnPosition;
        if (hardmode)
        {
            float distance = Vector2Int.Distance(spawnPosition, townPosition);
            int minX = Mathf.Max(1, spawnPosition.x - (int)minDistanceBetweenSpawnAndVillage);
            int maxX = Mathf.Min(mapWidth - 2, spawnPosition.x + (int)minDistanceBetweenSpawnAndVillage);
            int minY = Mathf.Max(1, spawnPosition.y - (int)minDistanceBetweenSpawnAndVillage);
            int maxY = Mathf.Min(mapHeight - 2, spawnPosition.y + (int)minDistanceBetweenSpawnAndVillage);
            while (distance > minDistanceBetweenSpawnAndVillage || distance < 3)
            {
                townPosition = new Vector2Int(Random.Range(minX, maxX), Random.Range(minY, maxY));
                distance = Vector2Int.Distance(spawnPosition, townPosition);
            }
        }
        else
        {
            while (Vector2Int.Distance(spawnPosition, townPosition) < minDistanceBetweenSpawnAndVillage)
            {
                townPosition = new Vector2Int(Random.Range(1, mapWidth - 2), Random.Range(1, mapHeight - 2));
            }
        }

    }
    private void GenerateTiles(bool hardmode)
    {
        //town - spwan
        tiles[townPosition.x, townPosition.y] = new TileData(townPosition, mapBorderTiles["town"]);
        tiles[spawnPosition.x, spawnPosition.y] = new TileData(spawnPosition, mapBorderTiles["spawn"]);

        //external walls
        for (int x = 1; x < mapWidth - 1; x++)
        {
            tiles[x, 0] = new TileData(new Vector2Int(x, 0), mapBorderTiles["bottomWall"]);
            tiles[x, mapHeight - 1] = new TileData(new Vector2Int(x, mapHeight - 1), mapBorderTiles["topWall"]);
        }
        for (int y = 1; y < mapHeight - 1; y++)
        {
            tiles[0, y] = new TileData(new Vector2Int(0, y), mapBorderTiles["leftWall"]);
            tiles[mapWidth - 1, y] = new TileData(new Vector2Int(mapWidth - 1, y), mapBorderTiles["rightWall"]);
        }
        tiles[0, 0] = new TileData(new Vector2Int(0, 0), mapBorderTiles["cornerWall"]);
        tiles[0, mapHeight - 1] = new TileData(new Vector2Int(0, mapHeight - 1), mapBorderTiles["cornerWall"]);
        tiles[mapWidth - 1, 0] = new TileData(new Vector2Int(mapWidth - 1, 0), mapBorderTiles["cornerWall"]);
        tiles[mapWidth - 1, mapHeight - 1] = new TileData(new Vector2Int(mapWidth - 1, mapHeight - 1), mapBorderTiles["cornerWall"]);

        //rest of map
        ExploreTileForGeneration(spawnPosition);
    }
    private TileData GenerateRandomTile(Vector2Int position)
    {
        GameAssets.CustomTile.Neighbours neighbours = new GameAssets.CustomTile.Neighbours(GameAssets.CustomTile.TileType.Any);

        if (TileExist(position + new Vector2Int(1, 0)))
        {
            neighbours.right = GetTile(position + new Vector2Int(1, 0)).neighbours.left;
        }
        if (TileExist(position + new Vector2Int(-1, 0)))
        {
            neighbours.left = GetTile(position + new Vector2Int(-1, 0)).neighbours.right;
        }
        if (TileExist(position + new Vector2Int(0, 1)))
        {
            neighbours.up = GetTile(position + new Vector2Int(0, 1)).neighbours.down;
        }
        if (TileExist(position + new Vector2Int(0, -1)))
        {
            neighbours.down = GetTile(position + new Vector2Int(0, -1)).neighbours.up;
        }
        List<GameAssets.CustomTile> compatibleTiles = GetCompatibleTiles(neighbours);
        if (compatibleTiles.Count == 0)
        {
            Debug.LogError("no compatible tiles at pos " + position);
            return new TileData(position, mapBorderTiles["grass"]);
        }
        else
        {
            return new TileData(position, compatibleTiles[Random.Range(0, compatibleTiles.Count)]);
        }
    }

    private List<GameAssets.CustomTile> GetCompatibleTiles(GameAssets.CustomTile.Neighbours neighbours)
    {
        List<GameAssets.CustomTile> compatibleTiles = new List<GameAssets.CustomTile>();
        foreach (GameAssets.CustomTile tile in GameAssets.i.tiles)
        {
            if ((tile.tileType != GameAssets.CustomTile.TileType.Town && tile.tileType != GameAssets.CustomTile.TileType.Spawner) &&
                (tile.neighbours.up == GameAssets.CustomTile.TileType.Any || neighbours.up == GameAssets.CustomTile.TileType.Any || tile.neighbours.up == neighbours.up) &&
                (tile.neighbours.down == GameAssets.CustomTile.TileType.Any || neighbours.down == GameAssets.CustomTile.TileType.Any || tile.neighbours.down == neighbours.down) &&
                (tile.neighbours.left == GameAssets.CustomTile.TileType.Any || neighbours.left == GameAssets.CustomTile.TileType.Any || tile.neighbours.left == neighbours.left) &&
                (tile.neighbours.right == GameAssets.CustomTile.TileType.Any || neighbours.right == GameAssets.CustomTile.TileType.Any || tile.neighbours.right == neighbours.right))
            {
                compatibleTiles.Add(tile);
            }
        }
        return compatibleTiles;
    }

    private void ExploreTileForGeneration(Vector2Int position)
    {
        if (position == townPosition)
        {
            pathCreated = true;
            return;
        }
        if (position != spawnPosition)
        {
            if (!IsInbound(position) || TileExist(position))
            {
                return;
            }
            if (!pathCreated)
            {
                tiles[position.x, position.y] = new TileData(position, mapBorderTiles["grass"]);
            }
            else
            {
                tiles[position.x, position.y] = GenerateRandomTile(position);
            }
        }

        List<int> sideToExplore = new List<int> { 0, 1, 2, 3 };

        while (sideToExplore.Count() > 0)
        {
            int side = sideToExplore[Random.Range(0, sideToExplore.Count)];
            sideToExplore.Remove(side);

            switch (side)
            {
                case 0:
                    ExploreTileForGeneration(position + new Vector2Int(1, 0));
                    break;
                case 1:
                    ExploreTileForGeneration(position + new Vector2Int(-1, 0));
                    break;
                case 2:
                    ExploreTileForGeneration(position + new Vector2Int(0, 1));
                    break;
                case 3:
                    ExploreTileForGeneration(position + new Vector2Int(0, -1));
                    break;
            }
        }


    }

    private TileData GetTile(Vector2Int position)
    {
        return tiles[position.x, position.y];
    }

    private void generateTilemap()
    {
        foreach (TileData tile in tiles)
        {
            tilemap.SetTile((Vector3Int)tile.position, tile.tileBase);
        }
    }

    public class TileData
    {
        public Vector2Int position;
        public GameAssets.CustomTile.Neighbours neighbours;
        public TileBase tileBase;
        public GameAssets.CustomTile.TileType tileType;
        public bool canWalk;
        public bool canBuild;

        public TileData(Vector2Int position, GameAssets.CustomTile tile)
        {
            this.position = position;
            neighbours = tile.neighbours;
            tileBase = tile.tileBase;
            tileType = tile.tileType;
            canWalk = tile.canWalk;
            canBuild = tile.canBuild;
        }
    }

    protected class PathTile
    {
        public Vector2Int position;
        public float value;
        public int creationOrder;

        private static int cpt;

        public PathTile(Vector2Int position, float value = 0)
        {
            this.position = position;
            this.value = value;
            this.creationOrder = cpt;
            cpt++;
        }
    }

}
