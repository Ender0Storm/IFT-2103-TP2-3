using Game.playerInformation;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapBuilder : MonoBehaviour
{
    public Tilemap temporaryMap;

    private Vector2Int temporarySpawnPosition = new Vector2Int(1,21);
    private Vector2Int temporaryTownPosition = new Vector2Int(32,4);
    private int temporaryHeight = 23;
    private int temporaryWidth = 38;

    private Tilemap tilemap;
    private Grid grid;
    void Start()
    {
        BuildMap();
    }


    public void BuildMap()
    {
        /***  Temporary before bulding it with code   ***/
        tilemap = Instantiate(temporaryMap,transform);
        tilemap.name = "Map";
        grid = tilemap.AddComponent<Grid>();

        transform.localPosition += new Vector3(-temporaryWidth /2, -temporaryHeight /2, 0);

        tilemap.AddComponent<SetWorldBounds>();

        //spawner.transform.localPosition = (Vector3Int)temporarySpawnPosition + grid.cellSize/2;
        //town.transform.localPosition = (Vector3Int)temporaryTownPosition + grid.cellSize/ 2;
    }

    public bool canWalk(Vector2Int position)
    {
        TileBase tile = getTileBase(position);
        if(tile != null)
        {
            return tile.name != "Wall";
        }
        return false;
    }

    public Vector2Int getSpawnPosition()
    {
        return temporarySpawnPosition;
    }
    public Vector2Int getTownPosition()
    {
        return temporaryTownPosition;
    }

    public int getMapHeight()
    {
        return temporaryHeight;
    }

    public int getMapWidth()
    {
        return temporaryWidth;
    }

    private TileBase getTileBase(Vector2Int position)
    {
        if(position.x >= tilemap.origin.x && 
            position.y >= tilemap.origin.y &&
            position.x < tilemap.origin.x + tilemap.size.x &&
            position.y < tilemap.origin.y + tilemap.size.y)
        {
            return tilemap.GetTile((Vector3Int)position);
        }
        return null;
    }


}
