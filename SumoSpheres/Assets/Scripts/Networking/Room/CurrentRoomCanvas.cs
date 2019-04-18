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
        if(PlayerNetwork.getCharSelect()) {
            PhotonNetwork.LoadLevel(1);
        }
        else {
            PhotonNetwork.LoadLevel(2);
        }
    }
}
