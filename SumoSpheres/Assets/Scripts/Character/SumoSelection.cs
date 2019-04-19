using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Presets;

public class SumoSelection : MonoBehaviour
{
    public Preset m_TransformPreset;
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
        
        m_TransformPreset.ApplyTo(m_SumoArray[index].GetComponent<Transform>());
        m_SumoArray[index].SetActive(true);
    }
}
