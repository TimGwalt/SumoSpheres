using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobbyNetwork : MonoBehaviourPunCallbacks
{
    void Start()
    {
        print("Connecting to server...");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("Connected to master.");
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.NickName = PlayerNetwork.m_Instance.m_Name;
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    // TODO: Move UI canvas logic to this function
    public override void OnJoinedLobby()
    {
        print("Joined master lobby.");
    }
}
