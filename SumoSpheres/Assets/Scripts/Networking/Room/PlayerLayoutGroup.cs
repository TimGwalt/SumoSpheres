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

    // Called by Photon whenever the master client has switched players. Parameter newMasterClient represents the player that
    // is the new master client.
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        print("Master client switched!");
    }

    // Called by Photon whenever the client has joined a room. This is called whether the player created the room or not.
    public override void OnJoinedRoom()
    {
        // Destroy any lefover player listings that may be in the player layout group.
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Store the players that are in the current room and notify the layout group that each player is in the room.
        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Length; i++)
        {
            PlayerJoinedRoom(players[i]);
        }
    }

    // Called by Photon whenever a player enters the current room. Parameter newPlayer represents the player that
    // joined the room.
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PlayerJoinedRoom(newPlayer);
    }

    // Called by Photon whenever a player leaves the current room. Parameter player represents the player that left
    // the room.
    public override void OnPlayerLeftRoom(Player player)
    {
        PlayerLeftRoom(player);
    }

    // Called whenever a player has joined the room. Parameter player is the player that has joined the room.
    private void PlayerJoinedRoom(Player player)
    {
        if (player == null)
            return;
        
        // Remove the player from the list if the player is already in the room to avoid adding duplicate players
        // to the player layout group.
        PlayerLeftRoom(player);

        // Instantiate the player listing game object in the UI and set the parent equal to the player layout group.
        GameObject playerListingObject = Instantiate(PlayerListingPrefab);
        playerListingObject.transform.SetParent(transform, false);

        // Apply the player info to the player listing.
        PlayerListing playerListing = playerListingObject.GetComponent<PlayerListing>();
        playerListing.ApplyPhotonPlayer(player);

        // Add the player listing to the list of player listings.
        PlayerListings.Add(playerListing);
    }

    // Called whenever a player has left the room. Parameter player is the player that left the room.
    private void PlayerLeftRoom(Player player)
    {
        // Determine if the player is in the list of player listings. This will be -1 if the player is not found.
        int index = PlayerListings.FindIndex(x => x.m_Player == player);

        // If the player has been found in the list of player listings, destroy the player's respective player listing game object
        // and remove it from the list of player listings.
        if (index != -1)
        {
            Destroy(PlayerListings[index].gameObject);
            PlayerListings.RemoveAt(index);
        }
    }

    // Called whenever the room state button is pressed.
    public void OnClickRoomState()
    {
        // Only perform this action on the master client.
        if (!PhotonNetwork.IsMasterClient)
            return;
        
        // Toggle the current room's open and visible status.
        PhotonNetwork.CurrentRoom.IsOpen = !PhotonNetwork.CurrentRoom.IsOpen;
        PhotonNetwork.CurrentRoom.IsVisible = !PhotonNetwork.CurrentRoom.IsVisible;

        // Toggle the current room's UI room state button Text and styling.
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

    // Called whenever the leave room button is pressed.
    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
