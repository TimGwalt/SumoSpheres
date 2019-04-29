using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobbyNetwork : MonoBehaviourPunCallbacks
{
    // On component start, connect to the Photon master server.
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            print("Connecting to server...");
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // Called by Photon when the client has successfully connected to the master server.
    public override void OnConnectedToMaster()
    {
        print("Connected to master.");
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.NickName = PlayerNetwork.m_Instance.m_Name;
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    // Called by Photon when the client has successfully connected to the master lobby.
    public override void OnJoinedLobby()
    {
        print("Joined master lobby.");
    }
}
