﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{
    public float speed;
    public float jumpSpeed;
    private float distanceToGound;
    private int lives = 3;
    private Rigidbody playerRB;
    private SphereCollider playerCollider;
    Transform cameraTransform;
    public Canvas canvas;
    public Text livesText;
    public Text playerCounterText;
    [SyncVar]
    public int playersConnected;

    public override void OnStartLocalPlayer()
    {
        //Find the canvas object
        GameObject tempObject = GameObject.Find("Menu Canvas");
        if (tempObject != null)
        {
            //If object found , get the Canvas component from it.
            canvas = tempObject.GetComponent<Canvas>();
            if (canvas == null)
            {
                Debug.Log("Could not locate Canvas component on " + tempObject.name);
            }
        }


        //Find the life counter object
        tempObject = GameObject.Find("Life Counter");
        if (tempObject != null)
        {
            //If object found , get the Canvas component from it.
            livesText = tempObject.GetComponent<Text>();
            if (livesText == null)
            {
                Debug.Log("Could not locate Text component on " + tempObject.name);
            }
        }
        livesText.text = "Lives: " + lives;
        
        //Find the player counter object
        tempObject = GameObject.Find("Player Count");
        if (tempObject != null)
        {
            //If object found , get the Canvas component from it.
            playerCounterText = tempObject.GetComponent<Text>();
            if (playerCounterText == null)
            {
                Debug.Log("Could not locate Text component on " + tempObject.name);
            }
        }
        CountPlayers();



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
        Camera.main.GetComponent<DetectObstacle>().player = this.transform;
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

            ScanForEscape();
            CountPlayers();
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distanceToGound + 0.1f);
    }
    
    public void die(){
        lives --;
        livesText.text = "Lives: " + lives;
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

    private void ScanForEscape()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (canvas.enabled)
            {
                canvas.enabled = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                canvas.enabled = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

            }
        }
    }

    private void CountPlayers()
    {
        if (isServer)
        {
            int count = 0;
            foreach (NetworkConnection con in NetworkServer.connections)
            {
                if (con != null)
                    count++;
            }

            playersConnected = count;
        }
        playerCounterText.text = "Enemies Remaining: " + (playersConnected - 1);

        
    }
}
