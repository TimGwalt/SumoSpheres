﻿using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager m_Instance;

    private PhotonView m_PhotonView;
    private List<PlayerStats> m_PlayerStats = new List<PlayerStats>();
    
    private void Awake()
    {
        m_Instance = this;
        m_PhotonView = GetComponent<PhotonView>();
    }

    // Todo: Perhaps make this return an error instead of recurse
    public PlayerStats GetPlayerStats(Player player)
    {
        int index = m_PlayerStats.FindIndex(x => x.m_Player == player);
        if (index != -1)
        {
            return m_PlayerStats[index];
        }
        else
        {
            AddPlayerStats(player);
            return GetPlayerStats(player);
        }
    }

    public void AddPlayerStats(Player player)
    {
        int index = m_PlayerStats.FindIndex(x => x.m_Player == player);
        if (index == -1)
        {
            m_PlayerStats.Add(new PlayerStats(player, 3));
        }
    }

    public void ModifyLives(Player player, int value)
    {
        int index = m_PlayerStats.FindIndex(x => x.m_Player == player);
        if (index != -1)
        {
            PlayerStats playerStats = m_PlayerStats[index];
            playerStats.m_Lives += value;
            PlayerNetwork.m_Instance.NewLives(player, playerStats.m_Lives);
        }
    }
    
    public void UpdateDeath()
    {
        PlayerNetwork.m_Instance.NewDeath();
    }
}

public class PlayerStats
{
    public PlayerStats(Player player, int lives)
    {
        m_Player = player;
        m_Lives = lives;
    }

    public readonly Player m_Player;
    public int m_Lives;
    public string m_SumoName;
    public Vector3 m_SpawnPoint;
}