using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
public class TeleportAbility : NetworkBasePlayerMovement
{
    public override void CheckInput()
    {
        float coolDownTimer = Time.deltaTime + 5; 
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDirection = input.normalized;
        if(Input.GetKey(KeyCode.E) && coolDownTimer < Time.time)
        {
           m_PlayerRB.position.Set(m_PlayerRB.transform.position.x * -1, m_PlayerRB.transform.position.y * -1, m_PlayerRB.transform.position.z);
           coolDownTimer = Time.deltaTime + 5; 
        }
    }
}