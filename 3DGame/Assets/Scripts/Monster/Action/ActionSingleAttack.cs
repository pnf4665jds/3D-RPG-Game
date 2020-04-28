using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSingleAttack : ActionBase
{
    // 這個Action用來讓怪物進行普通攻擊

    public float Damage;    // 傷害
    public string TriggerName;  // 要呼叫的Trigger名稱

    public override void Init()
    {
        animator.SetTrigger(TriggerName);
    }

    public override void Process()
    {
        
    }

    public override void Exit()
    {

    }

    protected override void DoOnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().GetHurt(Damage);
            //Debug.Log(collision.gameObject.GetComponent<Player>().GetCurHP());
        }
    }
}
