using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    // Transform of the target the camera should look at.
    public Transform target;
    float pitch;
    float yaw;
    public float sensitivity = 10;
    // Float storing how far away the camera should be from the target.
    public float offset = 2.0f;
    [Range(0.01f, 1.0f)]
    public float smoothFactor = 0.12f;
    // Vector containing the boundaries for the camera's pitch.
    public Vector2 pitchBounds = new Vector2(-10, 85);
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    void LateUpdate()
    {
        if(target)
        {
            yaw += Input.GetAxis("Mouse X") * sensitivity;
            pitch -= Input.GetAxis("Mouse Y") * sensitivity;
            pitch = Mathf.Clamp(pitch, pitchBounds.x, pitchBounds.y);

            // Smooth the camera rotation and rotate the camera's transform.
            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, smoothFactor);
            transform.eulerAngles = currentRotation;

            // Place the camera behind the target.
            transform.position = target.position - transform.forward * offset;
        }
    }
}
