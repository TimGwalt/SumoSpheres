using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAbility : NetworkBasePlayerMovement
{
    public override void CheckInput()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDirection = input.normalized;
        if(Input.GetKey(KeyCode.Space) & IsGrounded())
        {
            m_PlayerRB.AddForce(Vector3.up * m_JumpSpeed, ForceMode.Impulse);
        }
        if(inputDirection != Vector2.zero)
        {
            // Add force to the player dependent on input axes and camera direction.
            Vector3 movement = new Vector3(input.x, 0.0f, input.y);
            Vector3 actualMovement = m_CameraTransform.TransformDirection(movement);
            m_PlayerRB.AddForce(actualMovement * m_MoveSpeed);
        }
    }
}
