using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public float speed;
    public float jumpSpeed;
    private float distanceToGound;
    private int lives = 3;
    private Rigidbody playerRB;
    private SphereCollider playerCollider;
    Transform cameraTransform;

    public override void OnStartLocalPlayer()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Get a reference to the local player's rigid body and change the player's material color
        // to cyan.
        playerRB = GetComponent<Rigidbody>();
        playerCollider = GetComponent<SphereCollider>();
        GetComponent<MeshRenderer>().material.color = Color.cyan;

        // Set the main camera's target to the player's transform and get a reference to the main
        // camera's transform.
        Camera.main.GetComponent<ThirdPersonCamera>().target = this.transform;
        cameraTransform = Camera.main.transform;
        distanceToGound = playerCollider.bounds.extents.y;
    }

    private void FixedUpdate()
    {
        // Only allow the client to control their local player.
        if(isLocalPlayer)
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 inputDirection = input.normalized;
            if(Input.GetKey(KeyCode.Space) & IsGrounded())
            {
                playerRB.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            }
            if(inputDirection != Vector2.zero)
            {
                // Add force to the player dependent on input axes and camera direction.
                Vector3 movement = new Vector3(input.x, 0.0f, input.y);
                Vector3 actualMovement = cameraTransform.TransformDirection(movement);
                playerRB.AddForce(actualMovement * speed);
            }
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distanceToGound + 0.1f);
    }
    
    public void die(){
        lives --;
        if (lives > 0)
        {
            gameObject.transform.position = new Vector3(1f, 10f, 1f);
            //respawn
        }
        else
        {
            Destroy(gameObject);
            //display message/menu
            //update other clients
        }
    }
}
