using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
public class TeleportAbility : NetworkBasePlayerMovement
{
    float coolDownTimer = Time.time + 5; 
    
    // Overrides the checkInput method from the base class.
    // Changes the players loction to the opposite x and z axis coords.
    public override void CheckInput()
    {
        base.CheckInput();
       
            if(Input.GetKeyUp(KeyCode.E) & Time.time > coolDownTimer) // Checks for of "E" input and cool down.
            {
                Vector3 tele = new Vector3(m_PlayerRB.position.x * -1, m_PlayerRB.position.y, m_PlayerRB.position.z * -1);
                m_PlayerRB.position = tele;
                coolDownTimer = Time.time + 5; 
            }
    }

    // Updated once per frame. 
    // Used to keep track of time for ability cool down.
    private void update()
    {
        CheckInput();
    }
}