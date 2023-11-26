using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ServerRequests : NetworkBehaviour
{
    [ServerRpc(RequireOwnership = false)]
    public void SpawnTowerServerRpc(ulong ownerID)
    {
    }
}
