using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ServerRequests : NetworkBehaviour
{
    [ServerRpc(RequireOwnership = false)]
    public void SpawnTowerServerRpc(string prefabName, Vector2 position, ulong ownerID)
    {
        GameObject prefab = Resources.Load("Prefabs/Towers/" + prefabName) as GameObject;
        GameObject clone = Instantiate(prefab, position, Quaternion.identity);
        clone.GetComponent<NetworkObject>().SpawnWithOwnership(ownerID);
    }
}
