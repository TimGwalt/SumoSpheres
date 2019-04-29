using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SumoSelection : MonoBehaviour
{
    private GameObject[] m_SumoArray;
    private int index;

    // Populate the Sumo array with the children of the Sumo game object.
    private void Start()
    {
        m_SumoArray = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            m_SumoArray[i] = transform.GetChild(i).gameObject;
        }
    }

    // Called whenever the player clicks on an arrow button in the Sumo Select UI. Parameter isRight distinguishes if
    // the button is the right or left button.
    public void OnClickArrow(bool isRight)
    {
        // Deactivate current Sumo game object.
        m_SumoArray[index].SetActive(false);

        // Increment index if the right button is pressed, else decrement the index.
        if (isRight)
        {
            index++;
            if (index == m_SumoArray.Length)
                index = 0;
        }
        else
        {
            index--;
            if (index < 0)
                index = m_SumoArray.Length - 1;
        }

        // After new index has been calculated, set the active Sumo game object.
        m_SumoArray[index].SetActive(true);
    }

    // Called whenever the player clicks on the select button in the Sumo Select UI.
    public void OnClickSelect()
    {
        // Store the name and spawnpoint of the current active Sumo game Object and store it in the local player's
        // PlayerStats entry.
        PlayerStats localPlayerStats = PlayerManager.m_Instance.GetPlayerStats(PhotonNetwork.LocalPlayer);
        localPlayerStats.m_SumoName = m_SumoArray[index].name;
        localPlayerStats.m_SpawnPoint = m_SumoArray[index].GetComponent<Respawn>().m_SpawnTransform.position;

        // Load the Kyoto game scene.
        SceneManager.LoadScene(2);
    }
}
