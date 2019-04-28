using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform m_SpawnTransform;

    [SerializeField]
    private Transform m_PlayerTransform;

    private void Start()
    {
        m_PlayerTransform = this.transform;
    }

    // Sets player position to the spawn position and resets player velocity.
    public void KillPlayer()
    {
        m_PlayerTransform.position = m_SpawnTransform.position;
        m_PlayerTransform.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }
}
