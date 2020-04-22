using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionLand : ActionBase
{
    public float LandSpeed = 1;

    public override void Init()
    {
        animator.SetTrigger("Land");
    }

    public override void Process()
    {
        Land();
    }

    public override void Exit()
    {
        // 開啟重力
        GetComponent<Rigidbody>().useGravity = true;
    }

    /// <summary>
    /// 起飛
    /// </summary>
    private void Land()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.down, monsterInfo.MoveSpeed * Time.deltaTime * LandSpeed);
    }
}
