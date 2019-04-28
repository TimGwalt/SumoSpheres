using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRoomClick : MonoBehaviour
{
    // Hides the master client UI buttons from non-master clients and displays the room UI canvas.
    public void DisplayRoom()
    {
        GameObject.Find("StartMatchButton").SetActive(false);
        GameObject.Find("RoomState").SetActive(false);
        JoinCanvasManager.m_Instance.gameObject.GetComponent<Canvas>().enabled = false;
        RoomCanvasManager.m_Instance.gameObject.GetComponent<Canvas>().enabled = true;
    }
}
