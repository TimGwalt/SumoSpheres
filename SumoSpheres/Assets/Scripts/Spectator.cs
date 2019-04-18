using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class Spectator : NeworkBehavior {
 
 	//initial speed
	public int speed =2;
	public transform currentCamera;

 	
 	// Use this for initialization
 	void Start () {
 		//Get Camera.

 		NetworkBasePLayerMovement NetworkBasePLayerMovement = GetComponent<m_CameraTransform>();
 	}
  
 	// Update is called once per frame
 	void Update () {
 
 	//press shift to move faster
 	if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
 	{
    	speed = 4; 
  	}

 	else	
 	{
    	//if shift is not pressed, reset to default speed
    	speed =20; 
 	}

 	//For the following 'if statements' don't include 'else if', so that the user can press multiple buttons at the same time
 	//move camera to the left

 	if(Input.GetKey(KeyCode.A))
 	{
    	transform.position = transform.position + currentCamera.transform.right *-1 * speed * Time.deltaTime;
 	}
 
 	//move camera backwards
 	if(Input.GetKey(KeyCode.S))
 	{
    	transform.position = transform.position + currentCamera.transform.forward *-1 * speed * Time.deltaTime;

    }

 	//move camera to the right
 	if(Input.GetKey(KeyCode.D))
 	{
    	transform.position = transform.position + currentCamera.transform.right * speed * Time.deltaTime;
    }
 	//move camera forward
 	if(Input.GetKey(KeyCode.W))
 	{
  
    	transform.position = transform.position + currentCamera.transform.forward * speed * Time.deltaTime;
 	}
 	//move camera upwards
 	if(Input.GetKey(KeyCode.E))
 	{
    	transform.position = transform.position + currentCamera.transform.up * speed * Time.deltaTime;
 	}
 	//move camera downwards
 	if(Input.GetKey(KeyCode.Q))
 	{
    	transform.position = transform.position + currentCamera.transform.up * -1 *speed * Time.deltaTime;
 	}
 
 	}

}
