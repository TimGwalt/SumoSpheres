using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public float sensitivity = 10;
    public Transform target;
    public float offset = 2.0f;
    public Vector2 pitchBounds = new Vector2(-10, 85);
    [Range(0.01f, 1.0f)]
    public float smoothFactor = 0.12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    float yaw;
    float pitch;

    void LateUpdate()
    {
        if(target)
        {
            yaw += Input.GetAxis("Mouse X") * sensitivity;
            pitch -= Input.GetAxis("Mouse Y") * sensitivity;
            pitch = Mathf.Clamp(pitch, pitchBounds.x, pitchBounds.y);

            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, smoothFactor);
            transform.eulerAngles = currentRotation;

            transform.position = target.position - transform.forward * offset;
        }
    }
}
