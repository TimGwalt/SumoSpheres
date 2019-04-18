using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRoomClick : MonoBehaviour
{
    public void DisplayRoom()
    {
        JoinCanvasManager.m_Instance.gameObject.GetComponent<Canvas>().enabled = false;
        RoomCanvasManager.m_Instance.gameObject.GetComponent<Canvas>().enabled = true;
    }
}
