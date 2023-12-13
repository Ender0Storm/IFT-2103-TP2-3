using Game.playerInformation;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapBuilder : MonoBehaviour
{
    public Tilemap temporaryMap;

    [SerializeField]
    private GameObject spawner;
    [SerializeField]
    private GameObject town;

    private Vector3Int temporarySpawnPosition = new Vector3Int(1,21,0);
    private Vector3Int temporaryTownPosition = new Vector3Int(32,4,0);
    private List<Vector3Int> temporaryCantBuild = new List<Vector3Int>();
    private List<Vector3Int> temporaryCantWalk = new List<Vector3Int>();
    private int temporaryHeight = 23;
    private int temporaryWidth = 38;
    void Start()
    {
        createTempsCantWalk();
        createTempsCantBuild();
        BuildMap();
    }


    public void BuildMap()
    {
        /***  Temporary before bulding it with code   ***/
        Tilemap tilemap = Instantiate(temporaryMap);
        tilemap.transform.parent = transform;
        Grid grid = tilemap.AddComponent<Grid>();

        transform.localPosition += new Vector3(-temporaryWidth /2, -temporaryHeight /2, 0);

        tilemap.AddComponent<SetWorldBounds>();

        spawner.transform.localPosition = temporarySpawnPosition + grid.cellSize/2;
        town.transform.localPosition = temporaryTownPosition + grid.cellSize/ 2;
    }

    public bool CanWalk(Vector2Int position)
    {
        return IsInMap(position) && !temporaryCantWalk.Contains(((Vector3Int)position));
    }

    public bool CanBuild(Vector2Int position)
    {
        return IsInMap(position) && !temporaryCantBuild.Contains(((Vector3Int)position));
    }

    public bool IsInMap(Vector2Int position)
    {
        return position.x >= 0 && position.x < temporaryWidth && position.y >= 0 && position.y < temporaryHeight;
    }
    private void createTempsCantBuild()
    {
        temporaryCantBuild = temporaryCantWalk;
        temporaryCantBuild.Add(new Vector3Int(1,21 , 0));
        for (int x = 31; x <= 35; x++)
        {
            for (int y = 1; y <= 5; y++)
            {
                temporaryCantWalk.Add(new Vector3Int(x, y, 0));
            }
        }
    }
    private void createTempsCantWalk()
    {
        for (int x = 8; x <= 10; x++)
        {
            for(int y = 18; y <= 20; y++)
            {
                temporaryCantWalk.Add(new Vector3Int(x,y,0));
            }
        }

        for (int x = 15; x <= 19; x++)
        {
            temporaryCantWalk.Add(new Vector3Int(x, 6, 0));
        }

        for (int y = 8; y <= 13; y++)
        {
            temporaryCantWalk.Add(new Vector3Int(26, y, 0));
            temporaryCantWalk.Add(new Vector3Int(27, y, 0));
        }

        for (int x = 28; x <= 30; x++)
        {
            temporaryCantWalk.Add(new Vector3Int(x, 8, 0));
            temporaryCantWalk.Add(new Vector3Int(x, 9, 0));
        }

    }
}
