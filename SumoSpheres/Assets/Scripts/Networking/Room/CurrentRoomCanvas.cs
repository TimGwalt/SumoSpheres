using UnityEngine;
using Photon.Pun;

public class CurrentRoomCanvas : MonoBehaviour
{

    // Called whenever the master client clicks the start game button.
    public void OnClickStartGame()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.LoadLevel(1);
    }
}
