using System.Net;
using Game.menu;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class ConnectionAttempt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NetworkManager networkManager = NetworkManager.Singleton;
        if (Navigation.joinIP == "localhost")
        {
            networkManager.StartHost();
        }
        else
        {
            ((UnityTransport)networkManager.NetworkConfig.NetworkTransport).ConnectionData.Address = TryParseIpAddress(Navigation.joinIP);
            networkManager.StartClient();
        }
    }

    private string TryParseIpAddress(string ipAddress)
    {
        if (IPAddress.TryParse(ipAddress, out var address))
        {
            return ipAddress;
        }
        else { return "127.0.0.1"; }
    }
}
