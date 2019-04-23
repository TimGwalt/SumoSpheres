using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
public class SpeedAbility : NetworkBasePlayerMovement
{
    public float speedBoost = 2f;
    public override void CheckInput()
    {
        float coolDownTimer = Time.deltaTime + 5; 
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDirection = input.normalized;
        if(Input.GetKey(KeyCode.E) && coolDownTimer < Time.time)
        {
           m_PlayerRB.AddForce(Vector3.forward.normalized * speedBoost, ForceMode.Impulse);
           coolDownTimer = Time.deltaTime + 5; 
        }
    }
}
