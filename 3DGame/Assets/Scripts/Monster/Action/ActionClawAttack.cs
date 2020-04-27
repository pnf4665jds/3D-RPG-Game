using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionClawAttack : ActionBase
{
    // 這個Action用來讓火龍進行衝撞攻擊

    public float Damage;    // 傷害
   
    private bool alreadyHit = true;  // 是否已經攻擊到，防止過程中多次判定

    public override void Init()
    {
        animator.SetTrigger("ClawAttack");
        alreadyHit = false;
    }

    public override void Process()
    {
        
    }

    public override void Exit()
    {

    }

    protected override void DoOnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !alreadyHit)
        {
            alreadyHit = true;
            monsterInfo.GetDamage(100);
            Debug.Log(monsterInfo.CurrentHP);
        }
    }
}
