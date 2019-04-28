using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PauseMenuController : MonoBehaviourPun
{
    private Canvas m_PauseCanvas;

    // Ensures that the player can control only their own pause menu canvas
    private void Start()
    {
        if (photonView.IsMine)
        {
            m_PauseCanvas = GameObject.Find("Pause Menu").GetComponent<Canvas>();
        }
    }

    // Detect pause menu input and enable the canvas on escape keypress
    private void Update()
    {
        if (photonView.IsMine && Input.GetKeyDown(KeyCode.Escape))
        {
            m_PauseCanvas.enabled = !m_PauseCanvas.enabled;
        }
    }
}
