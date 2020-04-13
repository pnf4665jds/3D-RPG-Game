using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawAttack : ActionBase
{
    private bool colDetect = false;

    public override void Init()
    {
        colDetect = true;
        animator.SetTrigger("ClawAttack");
    }

    public override void Process()
    {
        
    }

    public override void Exit()
    {
        colDetect = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (controller.CurrentStateName != UseStateName)
            return;

        if(collision.gameObject.tag == "Player")
            Debug.Log("Hit" + gameObject.name);
    }
}
