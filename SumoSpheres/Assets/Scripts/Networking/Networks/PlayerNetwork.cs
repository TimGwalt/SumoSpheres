using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class PlayerNetwork : MonoBehaviour
{
    public static PlayerNetwork m_Instance;
    public string m_Name {get; private set;}

    private PhotonView m_PhotonView;
    private int m_PlayersInGame = 0;
    private int m_PlayersInSumoSelect = 0;
    private bool m_GameStarted = false;
    private bool m_InEndGame = false;
    private NetworkBasePlayerMovement m_CurrentPlayerMovement;
    
    private void Awake()
    {
        m_Instance = this;
        m_PhotonView = GetComponent<PhotonView>();
        m_Name = "Player#" + Random.Range(1,5);

        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;


        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && !m_InEndGame && m_GameStarted && m_PlayersInGame <=1)
        {
            PhotonNetwork.LoadLevel(3);
            m_PhotonView.RPC("RPC_LoadEndGameOthers", RpcTarget.Others);
            m_InEndGame = true;
        }
    }

    [PunRPC]
    private void RPC_LoadEndGameOthers()
    {
        PhotonNetwork.LoadLevel(3);
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Jayden_Character_Test")
        {
            m_PlayersInSumoSelect = 0;
            if (PhotonNetwork.IsMasterClient)
                MasterLoadedSumoSelect();
            else
                NonMasterLoadedSumoSelect();
        }
        else if (scene.name == "Jayden_Test")
        {
            m_PlayersInGame = 0;
            LoadedGame();
            // m_PhotonView.RPC("RPC_CreatePlayer", RpcTarget.All);
        }
    }

    private void MasterLoadedSumoSelect()
    {
        m_PhotonView.RPC("RPC_LoadedSumoSelectScene", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer);
        m_PhotonView.RPC("RPC_LoadSumoSelectOthers", RpcTarget.Others);
    }

    private void NonMasterLoadedSumoSelect()
    {
        m_PhotonView.RPC("RPC_LoadedSumoSelectScene", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer);
    }

    private void LoadedGame()
    {
        m_PhotonView.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer);
    }

    [PunRPC]
    private void RPC_LoadSumoSelectOthers()
    {
        PhotonNetwork.LoadLevel(1);
    }

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

    private void RPC_LoadGameSceneOthers()
    {
        PhotonNetwork.LoadLevel(2);
    }

    public void NewLives(Player player, int lives)
    {
        m_PhotonView.RPC("RPC_NewLives", player, lives);
    }

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

    [PunRPC]
    private void RPC_CreatePlayer()
    {
        PlayerStats localPlayerStats = PlayerManager.m_Instance.GetPlayerStats(PhotonNetwork.LocalPlayer);
        string sumoName = localPlayerStats.m_SumoName;
        Vector3 sumoPosition = localPlayerStats.m_SpawnPoint;
        GameObject gameObject = PhotonNetwork.Instantiate(Path.Combine("Prefabs", sumoName), sumoPosition, Quaternion.identity, 0);
        m_CurrentPlayerMovement = gameObject.GetComponent<NetworkBasePlayerMovement>();
    }

    public void NewDeath()
    {
        m_PhotonView.RPC("RPC_NewDeath", RpcTarget.MasterClient);
    }

    [PunRPC]
    private void RPC_NewDeath()
    {
        m_PlayersInGame--;
    }
}
