using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObstacle : MonoBehaviour
{
    public Transform m_Player;


    // Casts a ray from the current gameObject towards the player and sends a FadeObstacle message to the gameObject that is hit.
    void Update()
    {
        if (m_Player)
        {
            RaycastHit obstructionHit;
            if(Physics.Raycast(transform.position, m_Player.position - transform.position, out obstructionHit, Mathf.Infinity))
            {
                obstructionHit.transform.gameObject.SendMessage("FadeObstacle", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
