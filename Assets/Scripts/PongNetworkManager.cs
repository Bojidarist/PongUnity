using Mirror;
using UnityEngine;

public class PongNetworkManager : NetworkBehaviour
{
    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject ball;

    public bool isPlayerOneSpawned = false;
    public bool isPlayerTwoSpawned = false;

    private void Start()
    {
        if (NetworkServer.active)
        {
            if (NetworkServer.connections.Count == 1)
            {
                if (!isPlayerOneSpawned)
                {
                    isPlayerOneSpawned = true;
                    CmdSpawnPrefabs(ref playerOne);
                }
            }
            else if (NetworkServer.connections.Count == 2)
            {
                if (!isPlayerTwoSpawned)
                {
                    isPlayerTwoSpawned = true;
                    CmdSpawnPrefabs(ref playerTwo);
                    CmdSpawnPrefabs(ref ball);
                }
            }
        }
    }

    private void OnConnectedToServer()
    {
        if (NetworkServer.active)
        {
            if (NetworkServer.connections.Count == 1)
            {
                if (!isPlayerOneSpawned)
                {
                    isPlayerOneSpawned = true;
                    CmdSpawnPrefabs(ref playerOne);
                }
            }
            else if (NetworkServer.connections.Count == 2)
            {
                if (!isPlayerTwoSpawned)
                {
                    isPlayerTwoSpawned = true;
                    CmdSpawnPrefabs(ref playerTwo);
                }
            }
        }
    }

    [Command]
    public void CmdSpawnPrefabs(ref GameObject prefab)
    {
        prefab = Instantiate(prefab);
        NetworkServer.Spawn(prefab, connectionToClient);
    }
}
