using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class NetworkController : NetworkManager
{
    //One spawn point
    public NetworkStartPosition[] List;
    public static Transform spawnPosition;
    public int numberOfChars = 5;
    public GameObject[] characterList;
    public string resourcePath;

    GameObject prefab;
    
    //index of cur player from the character selection
    public static int curPlayer;



    //Runs at the begining of every call.
    private void Start()
    {

        //Make Array for childCount amount of characters
        characterList = new GameObject[numberOfChars];

        //Debug for initial value.
        //Intializeze the first spawn point.
        spawnPosition = List[0].transform;
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
     public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader ) { 
         // Read client message and receive index
         if (extraMessageReader != null) {
             var stream = extraMessageReader.ReadMessage<IntegerMessage> ();
             curPlayer = CharacterList.testInt;
             print("curPlayer is " + curPlayer);
         }
         //Select the prefab from the spawnable objects list
         var playerPrefab = spawnPrefabs[curPlayer];       
  
         // Create player object with prefab
         var player = Instantiate(playerPrefab, spawnPosition.position, Quaternion.identity) as GameObject;        
         
         // Add player object for connection
         NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
     }
    private Transform UpdateSpawnPosition(Transform spawn)
    {
        if (this.playerSpawnMethod == PlayerSpawnMethod.Random)
        {
            Debug.Log("Random");
            spawn = List[Random.Range(0, List.Length)].transform;
            spawnPosition = spawn;
            if (GameObject.FindWithTag("Player").transform == spawnPosition)
            {
                UpdateSpawnPosition(spawnPosition);
            }
            return spawnPosition;

        }

        if (this.playerSpawnMethod == PlayerSpawnMethod.RoundRobin)
        {
            Debug.Log("Round Robin");
            for (int i = 0; i <= List.Length; i++)
            {
                if (List[i].transform.Equals(spawn.transform))
                {
                    //Debug.Log("Last spawn was " + List[i].transform);
                    if (i + 1 == List.Length)
                    {
                        //Debug.Log("restarting list");
                        return List[0].transform;
                    }
                    //Debug.Log("Spawning at spawnPosition: " + List[i+1].transform);
                    return List[i + 1].transform;
                }

            }

        }
        return spawnPosition;
    }
}