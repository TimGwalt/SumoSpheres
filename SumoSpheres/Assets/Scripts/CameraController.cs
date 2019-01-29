using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;
    public Vector3 offset = new Vector3(0, 5, -5);

    // Update is called once per frame
    void LateUpdate()
    {
        if(player)
        {
            transform.position = player.position + offset;
            Debug.Log("Start initialPosition = " + offset);
        }
    }
}