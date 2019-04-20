using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SumoSelection : MonoBehaviour
{
    private GameObject[] m_SumoArray;
    private int index;

    private void Start()
    {
        m_SumoArray = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            m_SumoArray[i] = transform.GetChild(i).gameObject;
        }
    }

    public void OnClickArrow(bool isRight)
    {
        m_SumoArray[index].SetActive(false);

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
        m_SumoArray[index].SetActive(true);
    }

    public void OnClickSelect()
    {
        PlayerStats localPlayerStats = PlayerManager.m_Instance.GetPlayerStats(PhotonNetwork.LocalPlayer);
        localPlayerStats.m_SumoName = m_SumoArray[index].name;
        SceneManager.LoadScene(2);
    }
}
