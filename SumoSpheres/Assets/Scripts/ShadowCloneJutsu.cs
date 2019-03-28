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
        if(timeStamp <= Time.time) 
        {
            if (Input.GetKeyDown("c"))
            {
                Debug.Log("kage bushin no justu");
                PlayerClone = Instantiate(player, transform.position, Quaternion.identity) as GameObject;
                timeStamp = Time.time + coolDown;
                Destroy(PlayerClone, 3);
            }
        }

    }

}
