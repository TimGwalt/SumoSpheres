using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider c){
        if(c.tag == "Player"){
            var player = c.GetComponent<BasePlayerController>();
            player.die();
        }
    }
}
