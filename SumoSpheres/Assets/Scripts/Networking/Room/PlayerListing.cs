﻿using UnityEngine;
using Photon.Realtime;
using TMPro;

public class PlayerListing : MonoBehaviour
{
    public Player m_Player { get; private set; }
    [SerializeField]
    private TextMeshProUGUI m_PlayerName;
    private TextMeshProUGUI PlayerName
    {
        get { return m_PlayerName; }
    }
    
    public void ApplyPhotonPlayer(Player player)
    {
        PlayerName.text = player.NickName;
    }
}
