using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LobbyCanvas : MonoBehaviour
{
    [SerializeField]
    private RoomLayoutGroup m_RoomLayoutGroup;
    private RoomLayoutGroup RoomLayoutGroup
    {
        get { return m_RoomLayoutGroup; }
    }

    public void OnClickJoinRoom(string roomName)
    {
        if (PhotonNetwork.JoinRoom(roomName))
        {
            print("Joining " + roomName);
        }
        else
        {
            print("Join room failed.");
        }
    }
}
