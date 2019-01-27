using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public float speed;
    private Rigidbody rb;

    public override void OnStartLocalPlayer() {
        rb = GetComponent<Rigidbody>();
        GetComponent<MeshRenderer>().material.color = Color.cyan;
    }

    private void Start()
    {
        if(isLocalPlayer)
        {
            this.transform.GetChild(0).GetComponent<Camera>().enabled = true;
        }
    }

    private void FixedUpdate() {
        float moveX =  Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0.0f, moveZ);
        rb.AddForce(movement * speed);

        if(!isLocalPlayer)
        {
            return;
        }
    }
}
