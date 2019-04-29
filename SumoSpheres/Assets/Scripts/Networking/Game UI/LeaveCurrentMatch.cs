using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LeaveCurrentMatch : MonoBehaviour
{
    // Called when the player either clicks the exit button in the pause menu or clicks the menu button in the End Game UI.
    public void OnClickLeaveMatch()
    {
        // Iterate through DDOL objects and destroy them.
        foreach (GameObject DDOLObject in GameObject.FindGameObjectsWithTag("DDOL"))
        {
            Destroy(DDOLObject);
        }
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }
}
