using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomCanvasManager : MonoBehaviour
{
    public static RoomCanvasManager m_Instance;

    [SerializeField]
    private CurrentRoomCanvas m_currentRoomCanvas;
    public CurrentRoomCanvas CurrentRoomCanvas
    {
        get { return m_currentRoomCanvas; }
    }

    void Awake()
    {
        m_Instance = this;
        Debug.Log("RoomCanvasManager is awake!");
    }
}
