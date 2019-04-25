using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneAbility : NetworkBasePlayerMovement
{
    float coolDownTimer;
    GameObject m_PlayerClone;
    public GameObject m_Player;
    public float destroyTimer = 5f;
    public float coolDownLength = 5f;
    
    // Overrides the checkInput method from the base class.
    // Makes a clone of the player character that lasts a short time.
    public override void CheckInput()
    {
        base.CheckInput();
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDirection = input.normalized;

        if(Input.GetKeyUp(KeyCode.E) & Time.time > coolDownTimer) // Checks for of "E" input and cool down.
        {
            Vector3 movement = new Vector3(input.x, 0.0f, input.y);
            m_PlayerClone = Instantiate(m_Player, transform.position, Quaternion.identity) as GameObject;
            m_PlayerClone.GetComponent<Rigidbody>().AddForce(movement * 2, ForceMode.Impulse);
            Destroy(m_PlayerClone, destroyTimer);
            coolDownTimer = Time.time + coolDownLength; 
        }
    }
}
