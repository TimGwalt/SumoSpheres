using UnityEngine;
using Photon.Pun;

public class CurrentRoomCanvas : MonoBehaviour
{

    public void OnClickStartGame()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.LoadLevel(1);
    }
}
