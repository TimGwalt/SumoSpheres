using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LeaveCurrentMatch : MonoBehaviour
{
    public void OnClickLeaveMatch()
    {
        foreach (GameObject DDOLObject in GameObject.FindGameObjectsWithTag("DDOL"))
        {
            Destroy(DDOLObject);
        }
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }
}
