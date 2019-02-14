using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class NetworkController : NetworkManager
{
    //One spawn point
    public NetworkStartPosition[] List;
    public static Transform spawnPosition;

    //index of cur player from the character selection
    int curPlayer = CharacterList.index;
    

    //Runs at the begining of every call.
    private void Start()
    {
        //Debug for initial value.
        Debug.Log(curPlayer);
        //Intializeze the first spawn point.
        spawnPosition = List[0].transform;
        Debug.Log("index is: " + curPlayer + " coming into network");
    }


    //Called on client when connect
    public override void OnClientConnect(NetworkConnection conn)
    {

        // Create message to set the player
        IntegerMessage msg = new IntegerMessage(curPlayer);

        // Call Add player and pass the message
        ClientScene.AddPlayer(conn, 0, msg);
    }

    //Adding the player to the server.
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        //Select the prefab from the spawnable objects list
        var playerPrefab = spawnPrefabs[curPlayer];

        //get spawnposition.
        spawnPosition = UpdateSpawnPosition(spawnPosition);
        Debug.Log("Spawning at : " + spawnPosition);

        // Create player object with prefab
        GameObject player = Instantiate(playerPrefab,spawnPosition) as GameObject;

        //To make sure the correct player is being added
        Debug.Log("character deployed as player" + curPlayer);

        // Add player object for connection
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    private Transform UpdateSpawnPosition(Transform spawn)
    {

        if(this.playerSpawnMethod == PlayerSpawnMethod.Random)
        {
            Debug.Log("Random");
            return spawnPosition = List[Random.Range(0, List.Length)].transform;

        }

        if(this.playerSpawnMethod == PlayerSpawnMethod.RoundRobin)
        {
            Debug.Log("Round Robin");
            for(int i= 0; i<= List.Length; i++)
            {
                if(List[i].transform.Equals(spawn.transform))
                {
                    //Debug.Log("Last spawn was " + List[i].transform);
                    if(i+1 == List.Length)
                    {
                        //Debug.Log("restarting list");
                        return List[0].transform;
                    }
                        //Debug.Log("Spawning at spawnPosition: " + List[i+1].transform);
                        return List[i+1].transform;
                }
               
            }

        }
        return spawnPosition;
    }
}