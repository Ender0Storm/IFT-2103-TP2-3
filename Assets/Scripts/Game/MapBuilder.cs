using Game.playerInformation;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class MapBuilder : MonoBehaviour
{
    private bool pathCreated = false;
    public bool generationDone = false;

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
        BuildMap(bool.Parse(PlayerPrefs.GetString(PlayerPrefsKey.HARD_MODE, "false")), PlayerPrefs.GetInt(PlayerPrefsKey.SEED_KEY, 0));
    }


    public void BuildMap(bool hardmode = false, int seed = 0)
    {
        GenerateMapStats(hardmode, seed);

        GameObject map = new GameObject("Map");
        map.layer = 8;
        map.transform.parent = transform;
        TilemapRenderer renderer = map.AddComponent<TilemapRenderer>();
        renderer.sortingLayerName = "Background";

        tilemap = map.GetComponent<Tilemap>();

        TilemapCollider2D collider = map.AddComponent<TilemapCollider2D>();

        CompositeCollider2D compositeCollider = map.AddComponent<CompositeCollider2D>();
        compositeCollider.geometryType = CompositeCollider2D.GeometryType.Polygons;
        collider.usedByComposite = true;

        Rigidbody2D rigidbody = map.GetComponent<Rigidbody2D>();
        rigidbody.bodyType = RigidbodyType2D.Static;

        grid = map.AddComponent<Grid>();

        transform.localPosition += new Vector3(-mapWidth / 2, -mapHeight / 2, 0);

        GenerateTiles(hardmode);

        GenerateTilemap();

        generationDone = true;
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
        Random.InitState(seed);
        mapHeight = Random.Range(20, 30);
        mapWidth = Random.Range(30, 40);

        if (hardmode)
        {
            mapHeight = Random.Range(mapHeight / 2, mapHeight);
            mapWidth = Random.Range(mapWidth / 2, mapWidth);
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

        GameObject spawn = new GameObject("Spawn");
        spawn.layer = 7;
        spawn.transform.parent = transform;
        spawn.transform.position = new Vector3(spawnPosition.x + 0.5f, spawnPosition.y + 0.5f);
        spawn.AddComponent<BoxCollider2D>();
        BoxCollider2D spawnCollider = spawn.AddComponent<BoxCollider2D>();
        spawnCollider.size = new Vector2(0.95f, 0.95f);

        GameObject town = new GameObject("Town");
        town.layer = 7;
        town.transform.parent = transform;
        town.transform.position = new Vector3(townPosition.x + 0.5f, townPosition.y + 0.5f);
        BoxCollider2D townCollider = town.AddComponent<BoxCollider2D>();
        townCollider.size = new Vector2(0.95f, 0.95f);
        SoundManager.PlayFoley(SoundManager.Sound.TownFoley, new Vector3(townPosition.x + 0.5f - mapWidth/2, townPosition.y + 0.5f - mapHeight / 2));
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
    private TileData GenerateRandomTile(Vector2Int position, bool hasToBeWalkable = false, bool hasToBeBuildable = false)
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

        List<GameAssets.CustomTile> compatibleTiles = GetCompatibleTiles(neighbours, hasToBeWalkable, hasToBeBuildable);
        if (compatibleTiles.Count == 0)
        {
            //Debug.LogError("no compatible tiles at pos " + position);
            return new TileData(position, mapBorderTiles["grass"]);
        }
        return new TileData(position, compatibleTiles[Random.Range(0, compatibleTiles.Count)]);
    }

    private List<GameAssets.CustomTile> GetCompatibleTiles(GameAssets.CustomTile.Neighbours neighbours, bool hasToBeWalkable = false, bool hasToBeBuildable = false)
    {
        List<GameAssets.CustomTile> compatibleTiles = new List<GameAssets.CustomTile>();
        foreach (GameAssets.CustomTile tile in GameAssets.i.tiles)
        {
            if ((!hasToBeWalkable || tile.canWalk) && (!hasToBeBuildable || tile.canBuild) &&
                (tile.tileType != GameAssets.CustomTile.TileType.Town && tile.tileType != GameAssets.CustomTile.TileType.Spawner) &&
                (tile.neighbours.up == GameAssets.CustomTile.TileType.Any || neighbours.up == GameAssets.CustomTile.TileType.Any || tile.neighbours.up == neighbours.up) &&
                (tile.neighbours.down == GameAssets.CustomTile.TileType.Any || neighbours.down == GameAssets.CustomTile.TileType.Any || tile.neighbours.down == neighbours.down) &&
                (tile.neighbours.left == GameAssets.CustomTile.TileType.Any || neighbours.left == GameAssets.CustomTile.TileType.Any || tile.neighbours.left == neighbours.left) &&
                (tile.neighbours.right == GameAssets.CustomTile.TileType.Any || neighbours.right == GameAssets.CustomTile.TileType.Any || tile.neighbours.right == neighbours.right))
            {
                compatibleTiles.Add(tile);
            }
        }
        if (compatibleTiles.Contains(mapBorderTiles["grass"]) &&
            (hasToBeWalkable && Random.Range(0f, 1f) < 0.7 ||    //increase the chances of grass if needs walkable (70%)
            (!hasToBeWalkable && Random.Range(0f, 1f) < 0.95)))  //else increase the chances of grass with 30%
        {
            return new List<GameAssets.CustomTile> { mapBorderTiles["grass"] };
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
            tiles[position.x, position.y] = GenerateRandomTile(position, !pathCreated);
        }
        Vector2Int up = position + new Vector2Int(0, 1);
        Vector2Int down = position + new Vector2Int(0, -1);
        Vector2Int left = position + new Vector2Int(-1, 0);
        Vector2Int right = position + new Vector2Int(1, 0);

        Dictionary<Vector2Int, float> distances = new Dictionary<Vector2Int, float>
        {
            { up,Vector2.Distance(up, townPosition)},
            { down, Vector2.Distance(down, townPosition)},
            { left,Vector2.Distance(left, townPosition)},
            { right,Vector2.Distance(right, townPosition)}
        };

        while (distances.Count() > 0)
        {
            Vector2Int best = distances.OrderBy(pos => pos.Value).First().Key;
            distances.Remove(best);
            ExploreTileForGeneration(best);
        }


    }

    private TileData GetTile(Vector2Int position)
    {
        return tiles[position.x, position.y];
    }

    private void GenerateTilemap()
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
