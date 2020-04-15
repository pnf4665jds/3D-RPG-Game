using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionClawAttack : ActionBase
{
    // 是否已經攻擊到，防止過程中多次判定
    private bool alreadyHit = false;

    public override void Init()
    {
        animator.SetTrigger("ClawAttack");
    }

    public override void Process()
    {
        
    }

    public override void Exit()
    {
        alreadyHit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !alreadyHit)
        {
            alreadyHit = true;
            monsterInfo.GetDamage(100);
            Debug.Log(monsterInfo.CurrentHP);
        }
    }
}
