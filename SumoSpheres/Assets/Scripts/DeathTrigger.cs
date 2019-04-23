using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PhotonView photonView = other.GetComponent<PhotonView>();
        if (photonView != null)
        {
            PlayerManager.m_Instance.ModifyLives(photonView.Owner, -1);
            PlayerStats currentPlayerStats = PlayerManager.m_Instance.GetPlayerStats(photonView.Owner);
            if (currentPlayerStats.m_Lives > 0)
            {
                other.GetComponent<Respawn>().KillPlayer();
            }
        }
    }
}
