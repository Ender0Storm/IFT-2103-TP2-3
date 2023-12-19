using Game.pathFinding;
using Game.playerInformation;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class MapBuilder : MonoBehaviour
{
    public Tilemap temporaryMap;

    private Vector2Int spawnPosition;
    private Vector2Int townPosition;
    private int mapHeight;
    private int mapWidth;

    private Tilemap tilemap;
    private Grid grid;

    private TileData[,] tiles;
    void Start()
    {
        BuildMap();
    }


    public void BuildMap()
    {
        generateMapStats();

        GameObject map = new GameObject("Map");
        map.transform.parent = transform;
        map.AddComponent<TilemapRenderer>();

        tilemap = map.GetComponent<Tilemap>();
        grid = map.AddComponent<Grid>();

        transform.localPosition += new Vector3(-mapWidth / 2, -mapHeight / 2, 0);

        generateTiles();

        generateTilemap();

        tilemap.AddComponent<SetWorldBounds>();
    }

    public bool canWalk(Vector2Int position)
    {
        TileBase tile = getTileBase(position);
        if (tile != null)
        {
            return tile.name != "Wall";
        }
        return false;
    }

    public bool canBuild(Vector2Int position)
    {
        return canWalk(position);
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

    private void generateMapStats()
    {
        mapHeight = Random.Range(10, 10);
        mapWidth = Random.Range(10, 17);
        float minDistanceBetweenSpawnAndVillage = Mathf.Sqrt(mapWidth * mapWidth + mapHeight * mapHeight) / 3; /* 1/3 of the diagonal of the map */

        tiles = new TileData[mapWidth, mapHeight];

        spawnPosition = new Vector2Int(Random.Range(1, (mapWidth - 2) / 2), Random.Range(1, (mapHeight - 2) / 2));
        townPosition = spawnPosition;
        while (Vector2Int.Distance(spawnPosition, townPosition) < minDistanceBetweenSpawnAndVillage)
        {
            townPosition = new Vector2Int(Random.Range(1, mapWidth - 2), Random.Range(1, mapHeight - 2));
        }
    }

    private void generateTiles()
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

        for (int x = 1; x < mapWidth - 1; x++)
        {
            for (int y = 1; y < mapHeight - 1; y++)
            {
                Vector2Int position = new Vector2Int(x, y);
                if (position != townPosition && position != spawnPosition)
                {
                    tiles[position.x, position.y] = new TileData(position, GameAssets.i.tiles[2]);
                }
            }
        }
    }

    private void generateTilemap()
    {
        foreach (TileData tile in tiles)
        {
            tilemap.SetTile((Vector3Int)tile.position, tile.tileBase);
        }
    }

    private TileBase getTileBase(Vector2Int position)
    {
        if (position.x >= tilemap.origin.x &&
            position.y >= tilemap.origin.y &&
            position.x < tilemap.origin.x + tilemap.size.x &&
            position.y < tilemap.origin.y + tilemap.size.y)
        {
            return tilemap.GetTile((Vector3Int)position);
        }
        return null;
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

}
