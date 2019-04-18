using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(!PhotonNetwork.IsMasterClient)
            return;
        
        PhotonView photonView = other.GetComponent<PhotonView>();
        if (photonView != null)
        {
            PlayerManager.m_Instance.ModifyLives(photonView.Owner, -1);
        }
    }
}
