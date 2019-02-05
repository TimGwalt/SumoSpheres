using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObstacle : MonoBehaviour
{
    public Transform player;
    private LayerMask fadeMask;

    void Start()
    {
        fadeMask = LayerMask.GetMask("FadeObstacle");
    }

    void Update()
    {
        if (player)
        {
            RaycastHit obstructionHit;
            if(Physics.Raycast(transform.position, player.position - transform.position, out obstructionHit, Mathf.Infinity))
            {
                Debug.Log(obstructionHit.transform.gameObject);
            }
        }
    }
}
