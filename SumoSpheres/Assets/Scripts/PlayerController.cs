using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public float speed;
    private Rigidbody rb;

    public override void OnStartLocalPlayer()
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<MeshRenderer>().material.color = Color.cyan;
        Camera.main.GetComponent<CameraController>().player = this.transform;
    }

    private void FixedUpdate()
    {
        if(isLocalPlayer)
        {
            float moveX =  Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveX, 0.0f, moveZ);
            rb.AddForce(movement * speed);
        }
    }
}
