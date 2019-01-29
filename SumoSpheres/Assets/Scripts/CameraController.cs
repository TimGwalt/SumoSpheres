using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;
    public Vector3 offset = new Vector3(0, 5, -5);

    [Range(0.01f, 1.0f)]
    public float smoothFactor = 0.5f;
    public bool enableRotation = true;
    public float rotationSpeed = 5.0f;

    // Update is called once per frame
    void LateUpdate()
    {
        if(player)
        {
            if(enableRotation)
            {
                Quaternion cameraXTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
                Quaternion cameraYTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * rotationSpeed, Vector3.left);
                offset = cameraXTurnAngle * cameraYTurnAngle * offset;
            }
            Vector3 newPosition = player.position + offset;
            transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);
            transform.LookAt(player);
        }
    }
}