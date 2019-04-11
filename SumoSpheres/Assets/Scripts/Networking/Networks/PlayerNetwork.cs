using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.IO;

public class PlayerNetwork : MonoBehaviour
{
    public static PlayerNetwork m_Instance;
    public string m_Name {get; private set;}
    private PhotonView m_PhotonView;
    private int m_PlayersInGame = 0;

    void Awake()
    {
        m_Instance = this;
        m_PhotonView = GetComponent<PhotonView>();
        m_Name = "Player#" + Random.Range(1000, 9999);

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
        m_PhotonView.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient);
        m_PhotonView.RPC("RPC_LoadGameOthers", RpcTarget.Others);
    }

    private void NonMasterLoadedGame()
    {
        m_PhotonView.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient);
    }

    [PunRPC]
    private void RPC_LoadGameOthers()
    {
        PhotonNetwork.LoadLevel(1);
    }

    [PunRPC]
    private void RPC_LoadedGameScene()
    {
        m_PlayersInGame++;
        if (m_PlayersInGame == PhotonNetwork.PlayerList.Length)
        {
            Debug.Log("All players have loaded the game!");
            m_PhotonView.RPC("RPC_CreatePlayer", RpcTarget.All);
        }
    }

    [PunRPC]
    private void RPC_CreatePlayer()
    {
        float randomHeight = Random.Range(1.5f, 7f);
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Base Network Player"), Vector3.up * randomHeight, Quaternion.identity, 0);
    }
}
