using Game.playerInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapBuilder : MonoBehaviour
{
    public Tilemap temporaryMap;
    public TileBase temporarySpawn;

    [SerializeField]
    private GameObject spawner;
    [SerializeField]
    private GameObject town;

    private Vector3Int temporarySpawnPosition = new Vector3Int(1,21,0);
    private Vector3Int temporaryTownPosition = new Vector3Int(32,4,0);
    void Start()
    {
        BuildMap();
    }


    public void BuildMap()
    {
        /***  Temporary before bulding it with code   ***/
        Tilemap tilemap = Instantiate(temporaryMap);
        Grid grid = tilemap.AddComponent<Grid>();
        tilemap.AddComponent<SetWorldBounds>();

        spawner.transform.localPosition = temporarySpawnPosition + grid.cellSize/2;
        town.transform.localPosition = temporaryTownPosition + grid.cellSize/ 2;
    }
}
