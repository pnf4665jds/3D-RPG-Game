using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAttack : ActionBase
{
    // 這個Action用來讓怪物進行普通攻擊

    private bool colDetect = false;

    public override void Init()
    {
        colDetect = true;
        animator.SetTrigger("Attack");
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

        if (collision.gameObject.tag == "Player")
            Debug.Log("Hit");
    }
}
