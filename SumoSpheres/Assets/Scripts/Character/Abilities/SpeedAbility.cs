using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
public class SpeedAbility : NetworkBasePlayerMovement
{
    public float speedBoost = 5f;
    float coolDownTimer; 
    public float coolDownLength = 5f;

    // Overrides the checkInput method from the base class.
    // Applies a force to increase speed in the direction 
    // the player is facing.
    public override void CheckInput()
    {
        base.CheckInput();
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDirection = input.normalized;
        if(Input.GetKeyUp(KeyCode.E) & coolDownTimer < Time.time) // Checks for input of "E" and cool down.
        {
            Vector3 movement = new Vector3(input.x, 0.0f, input.y);
            Vector3 actualMovement = m_CameraTransform.TransformDirection(movement);
            m_PlayerRB.AddForce(actualMovement * speedBoost, ForceMode.Impulse);
            coolDownTimer = Time.time + coolDownLength; 
        }
    }
}
