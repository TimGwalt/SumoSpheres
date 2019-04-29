using UnityEngine;
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
    
    // Sets the player and player name of a player listing. Parameter player is the player the player listing represents.
    public void ApplyPhotonPlayer(Player player)
    {
        m_Player = player;
        PlayerName.text = player.NickName;
    }
}
