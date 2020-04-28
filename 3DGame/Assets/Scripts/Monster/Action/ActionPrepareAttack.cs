using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPrepareAttack : ActionBase
{
    // Start is called before the first frame update

    public override void Init()
    {
        animator.SetBool("PrepareAttack", true);
       
    }

    public override void Process()
    {
        
    }

    public override void Exit()
    {
    }
}
