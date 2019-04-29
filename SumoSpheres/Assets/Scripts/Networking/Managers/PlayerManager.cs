using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager m_Instance;

    private PhotonView m_PhotonView;
    private List<PlayerStats> m_PlayerStats = new List<PlayerStats>();
    
    // When the component first loads, set the static Player Manager instance.
    private void Awake()
    {
        m_Instance = this;
        m_PhotonView = GetComponent<PhotonView>();
    }

    // Gets the player stats of a player. Parameter player is the player who's stats should be returned.
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

    // Adds the stats of a player to the list of player stats. Parameter player is the player who's stats should be
    // added to the list.
    public void AddPlayerStats(Player player)
    {
        int index = m_PlayerStats.FindIndex(x => x.m_Player == player);
        if (index == -1)
        {
            m_PlayerStats.Add(new PlayerStats(player, 5));
        }
    }

    // Modifies the lives of a player on the local player's machine and notifies the network that the lives have been modified.
    // Parameter player is the player who's lives are to be modified and parameter value is the value by which the player's lives
    // should be modified by.
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
    
    // Notifies the network that a player has died.
    public void UpdateDeath()
    {
        PlayerNetwork.m_Instance.NewDeath();
    }
}

// Class PlayerStats stores the information of a player.
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
