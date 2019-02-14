using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class CharacterList : MonoBehaviour
{
    public static GameObject[] characterList;
    public static int index;
    // Start is called before the first frame update
    void Start()
    {
        index = PlayerPrefs.GetInt("Character Selected");
        //Make Array for childCount amount of characters
        characterList = new GameObject[transform.childCount];

        //Fill characterList array with child at their childCount
        for (int i = 0; i < transform.childCount; i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;
        }

        foreach(GameObject go in characterList)
        {
            go.SetActive(false);
        }

        if (characterList[index])
        {
            characterList[index].SetActive(true);
        }

    }

    public void ToggleLeft()
    {   
        //Toggle off the curretn model
        characterList[index].SetActive(false);

        //moves index to the left
        index--;
        
        //if index moves too far left it cycles to the end of the list
        if (index < 0)
        {
            index = characterList.Length - 1;
        }
        
        //Toggle on the new model
        characterList[index].SetActive(true);

        Debug.Log("This is character: " + index);
    }

    public void Toggleright()
    {
        characterList[index].SetActive(false);

        index++;

        if(index == characterList.Length)
        {
            index = 0;
        }

        characterList[index].SetActive(true);

        Debug.Log("This is characher: " + index);

    }

    public void Confirm()
    {
        PlayerPrefs.SetInt("CharacterSelected", index);
        Debug.Log("You've picked character:" + index);
        SceneManager.LoadScene("Tim_Test");
        NetworkManager.singleton.GetComponent<NetworkController>().curPlayer = index;
        characterList[index].SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
