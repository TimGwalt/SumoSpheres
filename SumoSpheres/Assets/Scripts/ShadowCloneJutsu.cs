using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCloneJutsu : MonoBehaviour
{
    public GameObject player;
    GameObject PlayerClone;
    public float coolDown;
    private float timeStamp;

    void Update()
    {
        if (Input.GetKeyDown("c") & timeStamp <= Time.time )
        {
            PlayerClone = Instantiate(player, transform.position, Quaternion.identity) as GameObject;
            //Destroy(PlayerClone, 10);
            timeStamp = Time.time + coolDown;
        }
    }
}
