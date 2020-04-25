using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionFly : ActionBase
{
    // 這個Action用來讓怪物進行起飛的動作

    public float TakeOffSpeed = 1;

    public override void Init()
    {
        animator.SetTrigger("TakeOff");
        // 關閉重力
        GetComponent<Rigidbody>().useGravity = false;
    }

    public override void Process()
    {
        TakeOff();
    }

    public override void Exit()
    {
    }

    /// <summary>
    /// 起飛
    /// </summary>
    private void TakeOff()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.up, monsterInfo.MoveSpeed * Time.deltaTime * TakeOffSpeed);
    }
}
