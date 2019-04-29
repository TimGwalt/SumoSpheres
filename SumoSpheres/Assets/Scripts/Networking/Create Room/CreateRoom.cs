using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class CreateRoom : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TextMeshProUGUI m_RoomName;
    private TextMeshProUGUI RoomName
    {
        get { return m_RoomName;}
    }

    // Called when user clicks on the Create button.
    public void OnClickCreateRoom()
    {
        Photon.Realtime.RoomOptions roomOptions = new Photon.Realtime.RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 5 };
        if (PhotonNetwork.CreateRoom(RoomName.text, roomOptions, Photon.Realtime.TypedLobby.Default))
        {
            print("Create room successfully sent.");
        }
        else
        {
            print("Create room failed to send.");
        }
    }
    
    // Called by Photon when the server can not create the room.
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("Failed to create room. Code: " + returnCode + " | " + message);
    }

    // Called by Photon when the server successfully creates the room.
    public override void OnCreatedRoom()
    {
        print("Room joined successfully.");
        this.gameObject.GetComponentInParent<Canvas>().enabled = false;
        RoomCanvasManager.m_Instance.gameObject.GetComponent<Canvas>().enabled = true;
    }
}
