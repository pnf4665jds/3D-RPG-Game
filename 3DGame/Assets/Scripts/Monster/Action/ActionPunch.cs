﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPunch : ActionBase
{
    public float Damage;    // 傷害
    public GameObject Punch;
   

    // Start is called before the first frame update
    public override void Init()
    {
        animator.SetTrigger("Attacking");
        setPunchDamage(Damage);
    }

    public override void Process()
    {

    }

    public override void Exit()
    {

    }

    public void setPunchDamage(float hurtValue) {
        Punch.GetComponent<PunchDamager>().SetValue(gameObject, hurtValue);
    }
}
