using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCloneJutsu : MonoBehaviour
{
    public GameObject player;
    GameObject PlayerClone;

    public void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            PlayerClone = Instantiate(player, transform.position, Quaternion.identity) as GameObject;
        }


    }
}
