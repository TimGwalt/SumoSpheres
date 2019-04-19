using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LeaveCurrentMatch : MonoBehaviour
{
    public void OnClickLeaveMatch()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0); 
        Destroy(GameObject.Find("DDOL"));
    }
}
