using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinCanvasManager : MonoBehaviour
{
    public static JoinCanvasManager m_Instance;

    [SerializeField]
    private LobbyCanvas m_LobbyCanvas;
    public LobbyCanvas LobbyCanvas
    {
        get { return m_LobbyCanvas; }
    }

    void Awake()
    {
        m_Instance = this;
        Debug.Log("JoinCanvasManager is awake!");
    }
}
