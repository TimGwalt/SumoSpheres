using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public float speed;
    private Rigidbody rb;
    Transform cameraTransform;

    public override void OnStartLocalPlayer()
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<MeshRenderer>().material.color = Color.cyan;
        Camera.main.GetComponent<ThirdPersonCamera>().target = this.transform;
        cameraTransform = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        if(isLocalPlayer)
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 inputDirection = input.normalized;

            if(inputDirection != Vector2.zero)
            {
                Vector3 movement = new Vector3(input.x, 0.0f, input.y);
                Vector3 actualMovement = cameraTransform.TransformDirection(movement);
                rb.AddForce(actualMovement * speed);
            }
        }
    }
}
