using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRoomClick : MonoBehaviour
{
    public void DisplayRoom()
    {
        GameObject.Find("StartMatchButton").SetActive(false);
        GameObject.Find("RoomState").SetActive(false);
        JoinCanvasManager.m_Instance.gameObject.GetComponent<Canvas>().enabled = false;
        RoomCanvasManager.m_Instance.gameObject.GetComponent<Canvas>().enabled = true;
    }
}
