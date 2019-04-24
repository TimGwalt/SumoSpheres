using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    private Canvas m_PauseCanvas;

    private void Start()
    {
        m_PauseCanvas = GameObject.Find("Pause Menu").GetComponent<Canvas>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_PauseCanvas.enabled = !m_PauseCanvas.enabled;
        }
    }
}
