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

    void Start()
    {
        BuildMap(true, 0);
    }


    public void BuildMap(bool hardmode = false, int seed = 0)
    {
        generateMapStats(hardmode, seed);

        GameObject map = new GameObject("Map");
        map.transform.parent = transform;
        map.AddComponent<TilemapRenderer>();

        tilemap = map.GetComponent<Tilemap>();
        grid = map.AddComponent<Grid>();

        transform.localPosition += new Vector3(-mapWidth / 2, -mapHeight / 2, 0);

        generateTiles(hardmode);

        generateTilemap();

        tilemap.AddComponent<SetWorldBounds>();
    }

    public bool canWalk(Vector2Int position)
    {
        return tileExist(position) && tiles[position.x, position.y].canWalk;
    }

    public bool canBuild(Vector2Int position)
    {
        return tileExist(position) && tiles[position.x, position.y].canBuild;
    }

    public Vector2Int getSpawnPosition()
    {
        return spawnPosition;
    }
    public Vector2Int getTownPosition()
    {
        return townPosition;
    }

    public int getMapHeight()
    {
        return mapHeight;
    }

    public int getMapWidth()
    {
        return mapWidth;
    }

    private bool isInbound(Vector2Int position)
    {
        return position.x >= 0 && position.x < mapWidth && position.y >= 0 && position.y < mapHeight;
    }

    private bool tileExist(Vector2Int position)
    {
        return isInbound(position) && tiles[position.x, position.y] != null;
    }

    private void generateMapStats(bool hardmode = false, int seed = 0)
    {
        if (seed != 0)
        {
            Random.InitState(seed);
        }
        mapHeight = Random.Range(20, 30);
        mapWidth = Random.Range(20, 30);

        if (hardmode)
        {
            mapHeight -= Random.Range(mapHeight/4, mapHeight/2); 
            mapWidth -= Random.Range(mapWidth/4, mapHeight/2); 
        }
        float minDistanceBetweenSpawnAndVillage = Mathf.Sqrt(mapWidth * mapWidth + mapHeight * mapHeight) / 3; /* 1/3 of the diagonal of the map */

        tiles = new TileData[mapWidth, mapHeight];

        spawnPosition = new Vector2Int(Random.Range(1, (mapWidth - 2) / 2), Random.Range(1, (mapHeight - 2) / 2));
        townPosition = spawnPosition;
        if(hardmode)
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

    private void generateTiles(bool hardmode)
    {
        //town - spwan
        tiles[townPosition.x, townPosition.y] = new TileData(townPosition, GameAssets.i.tiles[0]);
        tiles[spawnPosition.x, spawnPosition.y] = new TileData(spawnPosition, GameAssets.i.tiles[1]);

        //external walls
        for (int x = 0; x < mapWidth; x++)
        {
            tiles[x, 0] = new TileData(new Vector2Int(x, 0), GameAssets.i.tiles[3]);
            tiles[x, mapHeight - 1] = new TileData(new Vector2Int(x, mapHeight - 1), GameAssets.i.tiles[3]);
        }
        for (int y = 0; y < mapHeight; y++)
        {
            tiles[0, y] = new TileData(new Vector2Int(0, y), GameAssets.i.tiles[3]);
            tiles[mapWidth - 1, y] = new TileData(new Vector2Int(mapWidth - 1, y), GameAssets.i.tiles[3]);
        }

        //rest of map
        exploreTileForGeneration(spawnPosition);
    }

    private TileData generateRandomTile(Vector2Int position)
    {
        GameAssets.CustomTile.Neighbours neighbours = new GameAssets.CustomTile.Neighbours(GameAssets.CustomTile.TileType.Any);

        if(tileExist(position + new Vector2Int(1,0))) 
        {
            neighbours.up = GetTile(position + new Vector2Int(1, 0)).neighbours.down;
        }
        if (tileExist(position + new Vector2Int(-1, 0)))
        {
            neighbours.down = GetTile(position + new Vector2Int(-1, 0)).neighbours.up;
        }
        if (tileExist(position + new Vector2Int(0, 1)))
        {
            neighbours.right = GetTile(position + new Vector2Int(0, 1)).neighbours.left;
        }
        if (tileExist(position + new Vector2Int(0, -1)))
        {
            neighbours.left = GetTile(position + new Vector2Int(0, -1)).neighbours.right;
        }
        List<GameAssets.CustomTile> compatibleTiles = getCompatibleTiles(neighbours);
        if(compatibleTiles.Count == 0)
        {
            Debug.LogError("no compatible tiles at pos " + position);
            return new TileData(position, GameAssets.i.tiles[3]);
        }
        else
        {
            return new TileData(position, compatibleTiles[Random.Range(0,compatibleTiles.Count)]);
        }
    }

    private List<GameAssets.CustomTile> getCompatibleTiles(GameAssets.CustomTile.Neighbours neighbours)
    {
        List<GameAssets.CustomTile> compatibleTiles = new List<GameAssets.CustomTile> ();
        foreach(GameAssets.CustomTile tile in GameAssets.i.tiles)
        {
            if((tile.tileType != GameAssets.CustomTile.TileType.Town && tile.tileType != GameAssets.CustomTile.TileType.Spawner) &&
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

    private void exploreTileForGeneration(Vector2Int position)
    {
        if (position == townPosition)
        {
            pathCreated = true;
            return;
        }
        if (position != spawnPosition)
        {
            if (!isInbound(position) || tileExist(position))
            {
                return;
            }
            if (!pathCreated)
            {
                tiles[position.x, position.y] = new TileData(position, GameAssets.i.tiles[2]);
            }
            else
            {
                tiles[position.x, position.y] = generateRandomTile(position);
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
                    exploreTileForGeneration(position + new Vector2Int(1, 0));
                    break;
                case 1:
                    exploreTileForGeneration(position + new Vector2Int(-1, 0));
                    break;
                case 2:
                    exploreTileForGeneration(position + new Vector2Int(0, 1));
                    break;
                case 3:
                    exploreTileForGeneration(position + new Vector2Int(0, -1));
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
