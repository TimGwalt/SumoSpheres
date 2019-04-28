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

    // Called whenever the player clicks on a room in the room browser UI. Parameter roomName is the name of the selected room.
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
