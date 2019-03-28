using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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
        PhotonNetwork.NickName = PlayerNetwork.m_Instance.m_Name;

        PhotonNetwork.JoinLobby(Photon.Realtime.TypedLobby.Default);
    }

    public override void OnJoinedLobby()
    {
        print("Joined master lobby.");
    }
}
