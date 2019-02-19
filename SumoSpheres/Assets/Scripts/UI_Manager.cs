using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public void ChangeScene(string scene)
    {
        //Disconnect client/host from the current game
        
        //Change scene to main menu
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
