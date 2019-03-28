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

    public override void OnRoomListUpdate(List<Photon.Realtime.RoomInfo> roomList)
    {
        foreach (Photon.Realtime.RoomInfo roomInfo in roomList)
        {
            RoomReceived(roomInfo);
        }

        RemoveOldRooms();
    }

    private void RoomReceived(Photon.Realtime.RoomInfo room)
    {
        int index = RoomListingButtons.FindIndex(x => x.m_RoomName == room.Name);

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

    private void RemoveOldRooms()
    {
        List<RoomListing> removeRooms = new List<RoomListing>();

        foreach (RoomListing roomListing in RoomListingButtons)
        {
            if (!roomListing.m_Updated)
                removeRooms.Add(roomListing);
            else
                roomListing.m_Updated = false;
        }

        foreach (RoomListing roomListing in removeRooms)
        {
            GameObject roomListingObject = roomListing.gameObject;
            RoomListingButtons.Remove(roomListing);
            Destroy(roomListingObject);
        }
    }
}
