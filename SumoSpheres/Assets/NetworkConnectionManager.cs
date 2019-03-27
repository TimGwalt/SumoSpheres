using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class NetworkConnectionManager : MonoBehaviourPunCallbacks
{
    public Button m_ButtonConnectMaster;
    public Button m_ButtonConnectRoom;

    public bool m_AttemptConnectToMaster;
    public bool m_AttemptConnectToRoom;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        m_AttemptConnectToMaster = false;
        m_AttemptConnectToRoom = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_ButtonConnectMaster != null)
        {
            m_ButtonConnectMaster.gameObject.SetActive(!PhotonNetwork.IsConnected && !m_AttemptConnectToMaster);
        }
        if (m_ButtonConnectRoom != null)
        {
            m_ButtonConnectRoom.gameObject.SetActive(PhotonNetwork.IsConnected && !m_AttemptConnectToMaster && !m_AttemptConnectToRoom);
        }
    }

    public void OnClickConnectToMaster()
    {
        PhotonNetwork.OfflineMode = false;
        PhotonNetwork.NickName = "PlayerName";
        // PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "v1";

        m_AttemptConnectToMaster = true;
        // PhotonNetwork.ConnectToMaster(ip,port,appid); //manual connection
        // Automatic connection based on the config file in Photon/PhotonUnityNetworking/Resources/PhotonServerSettings.asset
        PhotonNetwork.ConnectUsingSettings();
    }

    public void OnClickConnectToRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;

        m_AttemptConnectToRoom = true;
        //PhotonNetwork.CreateRoom("Peter's Game 1"); //Create a specific Room - Error: OnCreateRoomFailed
        //PhotonNetwork.JoinRoom("Peter's Game 1");   //Join a specific Room   - Error: OnJoinRoomFailed  
        PhotonNetwork.JoinRandomRoom();               //Join a random Room     - Error: OnJoinRandomRoomFailed
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        m_AttemptConnectToMaster = false;
        m_AttemptConnectToRoom   = false;
        Debug.Log(cause);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        m_AttemptConnectToMaster = false;
        Debug.Log("Connected to Master!");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        m_AttemptConnectToRoom = false;
        Debug.Log("Master: " + PhotonNetwork.IsMasterClient + " | Players In Room: " + PhotonNetwork.CurrentRoom.PlayerCount + " | RoomName: " + PhotonNetwork.CurrentRoom.Name);
        SceneManager.LoadScene("Jayden_Test");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        //no room available
        //create a room (null as a name means "does not matter")
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 20 });
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log(message);
        base.OnCreateRoomFailed(returnCode, message);
        m_AttemptConnectToRoom = false;
    }
}
