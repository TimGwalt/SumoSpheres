using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
public class TeleportAbility : NetworkBasePlayerMovement
{
    public override void CheckInput()
    {
        base.CheckInput();
        float coolDownTimer = Time.deltaTime + 5; 
        if(Input.GetKey(KeyCode.E) && coolDownTimer < Time.time)
        {
            Vector3 tele = new Vector3(m_PlayerRB.position.x * -1, m_PlayerRB.position.y, m_PlayerRB.position.z * -1);
            m_PlayerRB.position = tele;
            coolDownTimer = Time.deltaTime + 5; 
        }
    }
}