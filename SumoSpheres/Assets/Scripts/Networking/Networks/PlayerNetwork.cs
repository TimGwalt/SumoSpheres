using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class PlayerNetwork : MonoBehaviour
{
    public static PlayerNetwork m_Instance;
    public string m_Name {get; private set;}

    private string[] colors = {"Red", "Green", "Blue", "Orange", "Purple", "Yellow", "Salmon", "Pink", "Aquamarine", "Magenta"};
    private string[] animals = {"Rhino", "Cheetah", "Snake", "Lion", "Puppy", "Puma", "Platypus", "Weasel", "Rat", "Walrus", "Emu"};

    private int m_PlayersInGame = 0;
    private int m_PlayersInSumoSelect = 0;
    private bool m_GameStarted = false;
    private bool m_InEndGame = false;
    private NetworkBasePlayerMovement m_CurrentPlayerMovement;
    private PhotonView m_PhotonView;

    // On component start set the static PlayerNetwork instance, player name, and network settings.
    private void Awake()
    {
        m_Instance = this;
        m_PhotonView = GetComponent<PhotonView>();
        string color = colors[Random.Range(0, 9)];
        string animal = animals[Random.Range(0, 10)];
        m_Name = color + ' ' + animal;

        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;

        // Call OnSceneFinishedLoading whenever a scene is loaded.
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    // Check every frame if the game has ended and, if so, load the end game scene.
    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && !m_InEndGame && m_GameStarted && m_PlayersInGame <= 1)
        {
            PhotonNetwork.LoadLevel(3);
            m_PhotonView.RPC("RPC_LoadEndGameOthers", RpcTarget.Others);
            m_InEndGame = true;
        }
    }

    // RPC to load the end game scene on other clients.
    [PunRPC]
    private void RPC_LoadEndGameOthers()
    {
        PhotonNetwork.LoadLevel(3);
    }

    // Called whenever a scene has finished loading.
    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        // Determine the name of the loaded scene. If the scene is the Sumo Select scene, set the number of players in
        // sumo select and the number of players in the game to zero. Else, if the scene is the Kyoto game scene, call
        // the LoadedGame() method.
        if (scene.name == "Sumo Select")
        {
            m_PlayersInSumoSelect = 0;
            m_PlayersInGame = 0;
            if (PhotonNetwork.IsMasterClient)
                MasterLoadedSumoSelect();
            else
                NonMasterLoadedSumoSelect();
        }
        else if (scene.name == "Kyoto")
        {
            LoadedGame();
        }
    }

    // Called whenever the master client loads Sumo Select.
    private void MasterLoadedSumoSelect()
    {
        m_PhotonView.RPC("RPC_LoadedSumoSelectScene", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer);
        m_PhotonView.RPC("RPC_LoadSumoSelectOthers", RpcTarget.Others);
    }

    // Called whenever a non-master client loads Sumo Select.
    private void NonMasterLoadedSumoSelect()
    {
        m_PhotonView.RPC("RPC_LoadedSumoSelectScene", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer);
    }

    // Called whenever a player has loaded the Kyoto game scene.
    private void LoadedGame()
    {
        m_PhotonView.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer);
    }

    // Photon RPC that will load the Sumo Select scene on other for other clients.
    [PunRPC]
    private void RPC_LoadSumoSelectOthers()
    {
        PhotonNetwork.LoadLevel(1);
    }

    // Photon RPC that will add a player's info to the list of PlayerStats and update the number of players in Sumo Select.
    // Parameter player is the player who's info will be added to the list of PlayerStats.
    [PunRPC]
    private void RPC_LoadedSumoSelectScene(Player player)
    {
        PlayerManager.m_Instance.AddPlayerStats(player);
        m_PlayersInSumoSelect++;

        if (m_PlayersInSumoSelect == PhotonNetwork.PlayerList.Length)
        {
            Debug.Log("All players have loaded sumo select!");
        }
    }

    // Photon RPC that will update the number of players in the game and, if all players have joined, signal all other clients
    // to start the match and load into the game scene.
    [PunRPC]
    private void RPC_LoadedGameScene(Player player)
    {
        m_PlayersInGame++;

        if (m_PlayersInGame == PhotonNetwork.PlayerList.Length)
        {
            m_GameStarted = true;
            Debug.Log("All players have loaded game scene!");
            m_PhotonView.RPC("RPC_CreatePlayer", RpcTarget.All);
        }
    }

    // Photon RPC that will load the game scene on other clients.
    [PunRPC]
    private void RPC_LoadGameSceneOthers()
    {
        PhotonNetwork.LoadLevel(2);
    }

    // Public-facing method that sends an RPC that updates the lives of a player. Parameter player is the player who's lives
    // need to be updated and parameter lives represents the new number of lives the player has.
    public void NewLives(Player player, int lives)
    {
        m_PhotonView.RPC("RPC_NewLives", player, lives);
    }


    // Photon RPC that will destroy the player's game object if the player's number of lives has fallen below zero, else it will
    // update the player's lives with the update number of lives. Parameter lives is the player's new number of lives.
    [PunRPC]
    private void RPC_NewLives(int lives)
    {
        if (m_CurrentPlayerMovement == null)
            return;
        
        if (lives <= 0)
            PhotonNetwork.Destroy(m_CurrentPlayerMovement.gameObject);
        else
            m_CurrentPlayerMovement.m_Lives = lives;
    }

    // Photon RPC that will instantiate a player on the network based on the local player's PlayerStats information.
    [PunRPC]
    private void RPC_CreatePlayer()
    {
        PlayerStats localPlayerStats = PlayerManager.m_Instance.GetPlayerStats(PhotonNetwork.LocalPlayer);
        string sumoName = localPlayerStats.m_SumoName;
        Vector3 sumoPosition = localPlayerStats.m_SpawnPoint;
        GameObject gameObject = PhotonNetwork.Instantiate(Path.Combine("Prefabs", sumoName), sumoPosition, Quaternion.identity, 0);
        m_CurrentPlayerMovement = gameObject.GetComponent<NetworkBasePlayerMovement>();
    }

    // Public-facing method that calls an RPC that updates the number of players in the game on the master client.
    public void NewDeath()
    {
        m_PhotonView.RPC("RPC_NewDeath", RpcTarget.MasterClient);
    }

    // Photon RPC that will update the current number of players in the game.
    [PunRPC]
    private void RPC_NewDeath()
    {
        m_PlayersInGame--;
    }
}
