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
    private NetworkBasePlayerMovement m_CurrentPlayerMovement;

    public static string playerChoice;
    public static bool charSelect = false;


    public static bool getCharSelect() {
        return charSelect;
    }
    
    void Awake()
    {
        m_Instance = this;
        m_PhotonView = GetComponent<PhotonView>();
        m_Name = "Player#" + Random.Range(1,5);

        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;


        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Jayden_Test")
        {
            if (PhotonNetwork.IsMasterClient)
                MasterLoadedGame();
            else
                NonMasterLoadedGame();
        }
    }

    private void MasterLoadedGame()
    {
        m_PhotonView.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer);
        m_PhotonView.RPC("RPC_LoadGameOthers", RpcTarget.Others);
    }

    private void NonMasterLoadedGame()
    {
        m_PhotonView.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer);
    }

    [PunRPC]
    private void RPC_LoadGameOthers()
    {
        PhotonNetwork.LoadLevel(1);
    }

    [PunRPC]
    private void RPC_LoadedGameScene(Player player)
    {
        PlayerManager.m_Instance.AddPlayerStats(player);
        m_PlayersInGame++;

        if (m_PlayersInGame == PhotonNetwork.PlayerList.Length)
        {
            Debug.Log("All players have loaded the game!");
            m_PhotonView.RPC("RPC_CreatePlayer", RpcTarget.All);
        }
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
        if(charSelect != true) {
            int randomNum = Random.Range(1,5);
            Debug.Log(randomNum);
            playerChoice = "Player " + randomNum;
        }
        else {
            playerChoice = "Player " + (1 + CharacterList.getIndex());
            Debug.Log(playerChoice);
        }

        
        float randomHeight = Random.Range(1.5f, 7f);
        GameObject gameObject = PhotonNetwork.Instantiate(Path.Combine("Prefabs", playerChoice), Vector3.up * randomHeight, Quaternion.identity, 0);
        // GameObject gameObject = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Base Network Player"), Vector3.up * randomHeight, Quaternion.identity, 0);
        m_CurrentPlayerMovement = gameObject.GetComponent<NetworkBasePlayerMovement>();
        // PhotonNetwork.Instantiate(Path.Combine("Prefabs", playerChoice), Vector3.up * randomHeight, Quaternion.identity, 0);
    }
}
