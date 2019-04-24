using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneAbility : NetworkBasePlayerMovement
{
    float coolDownTimer;
    GameObject m_PlayerClone;
    public GameObject m_Player;
    public float destroyTimer = 5f;
    
    // Overrides the checkInput method from the base class.
    // Makes a clone of the player character that lasts a short time.
    public override void CheckInput()
    {
        base.CheckInput();
       
            if(Input.GetKeyUp(KeyCode.E) & Time.time > coolDownTimer) // Checks for of "E" input and cool down.
            {
                m_PlayerClone = Instantiate(m_Player, transform.position, Quaternion.identity) as GameObject;
                Destroy(m_PlayerClone, destroyTimer);
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
