using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
public class SpeedAbility : NetworkBasePlayerMovement
{
    public float speedBoost = 5f;
    public override void CheckInput()
    {
        base.CheckInput();
        float coolDownTimer = Time.deltaTime + 5; 
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDirection = input.normalized;
        if(Input.GetKeyUp(KeyCode.E) & coolDownTimer < Time.time)
        {
            Vector3 movement = new Vector3(input.x, 0.0f, input.y);
            Vector3 actualMovement = m_CameraTransform.TransformDirection(movement);
            //m_PlayerRB.AddForce(actualMovement * m_MoveSpeed * speedBoost);
            m_PlayerRB.AddForce(actualMovement * speedBoost, ForceMode.Impulse);
            coolDownTimer = Time.deltaTime + 5; 
        }
    }
}
