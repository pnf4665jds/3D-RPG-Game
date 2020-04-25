using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionClawAttack : ActionBase
{
    // 這個Action用來讓火龍進行衝撞攻擊

    // 是否已經攻擊到，防止過程中多次判定
    private bool alreadyHit = true;

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
