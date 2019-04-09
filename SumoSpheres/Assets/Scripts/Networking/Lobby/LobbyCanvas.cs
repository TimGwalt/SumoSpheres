using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    }
}
