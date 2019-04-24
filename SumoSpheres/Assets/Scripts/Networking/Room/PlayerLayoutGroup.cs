using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerLayoutGroup : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject m_playerListingPrefab;
    private GameObject PlayerListingPrefab
    {
        get { return m_playerListingPrefab; }
    }

    private List<PlayerListing> m_playerListings = new List<PlayerListing>();
    private List<PlayerListing> PlayerListings
    {
        get { return m_playerListings; }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        print("Master client switched!");
    }

    public override void OnJoinedRoom()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Length; i++)
        {
            PlayerJoinedRoom(players[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PlayerJoinedRoom(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player player)
    {
        PlayerLeftRoom(player);
    }

    private void PlayerJoinedRoom(Player player)
    {
        if (player == null)
            return;
        
        PlayerLeftRoom(player);

        GameObject playerListingObject = Instantiate(PlayerListingPrefab);
        playerListingObject.transform.SetParent(transform, false);

        PlayerListing playerListing = playerListingObject.GetComponent<PlayerListing>();
        playerListing.ApplyPhotonPlayer(player);

        PlayerListings.Add(playerListing);
    }

    private void PlayerLeftRoom(Player player)
    {
        int index = PlayerListings.FindIndex(x => x.m_Player == player);
        if (index != -1)
        {
            Destroy(PlayerListings[index].gameObject);
            PlayerListings.RemoveAt(index);
        }
    }

    public void OnClickRoomState()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        
        
        PhotonNetwork.CurrentRoom.IsOpen = !PhotonNetwork.CurrentRoom.IsOpen;
        PhotonNetwork.CurrentRoom.IsVisible = !PhotonNetwork.CurrentRoom.IsVisible;

        if (PhotonNetwork.CurrentRoom.IsOpen)
        {
            GameObject.Find("RoomState").GetComponentInChildren<TextMeshProUGUI>().text = "Hide Room";
            GameObject.Find("RoomState").GetComponentInChildren<TextMeshProUGUI>().alignment = TextAlignmentOptions.Midline;
        }
        else
        {
            GameObject.Find("RoomState").GetComponentInChildren<TextMeshProUGUI>().text = "Open Room";
            GameObject.Find("RoomState").GetComponentInChildren<TextMeshProUGUI>().alignment = TextAlignmentOptions.Capline;
        }
    }

    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
