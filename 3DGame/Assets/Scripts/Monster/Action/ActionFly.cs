using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionFly : ActionBase
{
    public override void Init()
    {
        animator.SetTrigger("TakeOff");
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
        // 關閉重力
        GetComponent<Rigidbody>().useGravity = false;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.up, monsterInfo.MoveSpeed * Time.deltaTime);
    }
}
