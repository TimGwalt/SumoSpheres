using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCloneJutsu : MonoBehaviour
{
    public GameObject player;
    GameObject PlayerClone;
    private float timeStamp;
    public float coolDown;
    public float sec = 5f;
  
    void Update()
    {
        if (timeStamp <= Time.time)
        {
            if (Input.GetKeyDown("c"))
            { 
              
                    Debug.Log("kage bushin no justu");
                    PlayerClone = Instantiate(player, transform.position, Quaternion.identity) as GameObject;
                    PlayerClone.GetComponent<ShadowCloneJutsu>().enabled = false;
                    Destroy(PlayerClone, sec);
                    timeStamp = Time.time + coolDown;

            }
        }
    }             
}
