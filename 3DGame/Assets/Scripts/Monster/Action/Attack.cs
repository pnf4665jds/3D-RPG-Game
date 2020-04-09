using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : ActionBase
{
    private bool colDetect = false;

    public override void Init()
    {
        colDetect = true;
    }

    public override void Process()
    {
        animator.SetTrigger("Attack");
    }

    public override void Exit()
    {
        colDetect = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit");
    }
}
