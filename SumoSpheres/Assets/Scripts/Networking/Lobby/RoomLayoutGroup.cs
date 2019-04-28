using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomLayoutGroup : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject m_RoomListingPrefab;
    private GameObject RoomListingPrefab
    {
        get { return m_RoomListingPrefab; }
    }
    private List<RoomListing> m_RoomListingButtons = new List<RoomListing>();
    private List<RoomListing> RoomListingButtons
    {
        get { return m_RoomListingButtons; }
    }

    // Called by photon whenever the room list has been updated. Parameter roomList is the updated list of rooms.
    public override void OnRoomListUpdate(List<Photon.Realtime.RoomInfo> roomList)
    {
        // Iterate through the updated room list and process the room info.
        foreach (Photon.Realtime.RoomInfo roomInfo in roomList)
        {
            RoomReceived(roomInfo);
        }

        // Remove old rooms from the local list of room listings.
        RemoveOldRooms();
    }

    // RoomReceived is called for every room in every updated room list. Parameter room is the room info of the received room.
    private void RoomReceived(Photon.Realtime.RoomInfo room)
    {
        // Find the index of the received room. This will be -1 if the room is not found.
        int index = RoomListingButtons.FindIndex(x => x.m_RoomName == room.Name);

        // Update the local list of room listings and the room listing UI if the room has not yet been received.
        if (index == -1)
        {
            if (room.IsVisible && room.PlayerCount < room.MaxPlayers)
            {
                GameObject roomListingObject = Instantiate(RoomListingPrefab);
                roomListingObject.transform.SetParent(transform, false);

                RoomListing roomListing = roomListingObject.GetComponent<RoomListing>();
                RoomListingButtons.Add(roomListing);

                index = (RoomListingButtons.Count - 1);
            }
        }

        // If the room is already in the local list of room listings, detect if the room has been removed. If the room
        // has been removed, set the updated flag to false, otherwise the room name has been updated so store the new
        // room name and set the updated flag to true.
        if (index != -1)
        {
            RoomListing roomListing = RoomListingButtons[index];
            if (room.RemovedFromList)
            {
                roomListing.m_Updated = false;
            }
            else
            {
                roomListing.SetRoomNameText(room.Name);
                roomListing.m_Updated = true;
            }
        }
    }

    // Called after every room list update.
    private void RemoveOldRooms()
    {
        // removeRooms is a list of rooms to be removed from the local list of room listings.
        List<RoomListing> removeRooms = new List<RoomListing>();

        // Iterate through the room listings in the list of room listing buttons and, if the room has not been
        // updated, add it to the list of room listings to be removed. Otherwise, reset the updated flag.
        foreach (RoomListing roomListing in RoomListingButtons)
        {
            if (!roomListing.m_Updated)
                removeRooms.Add(roomListing);
            else
                roomListing.m_Updated = false;
        }

        // Iterate through the room listings in the list of room listings to be removed and remove the room listings
        // from the local list of room listings and destroy the room listing game objects.
        foreach (RoomListing roomListing in removeRooms)
        {
            GameObject roomListingObject = roomListing.gameObject;
            RoomListingButtons.Remove(roomListing);
            Destroy(roomListingObject);
        }
    }
}
