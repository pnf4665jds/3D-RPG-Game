using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : ActionBase
{
    private MonsterInfo info;
    private GameObject target;

    public override void Init()
    {

        info = GetComponent<MonsterInfo>();
        target = controller.CurrentTarget;
    }

    public override void Process()
    {
        FollowPlayer();
    }

    public override void Exit()
    {
        target = null;
    }

    /// <summary>
    /// 跟隨玩家
    /// </summary>
    private void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, info.MoveSpeed * Time.deltaTime);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, target.transform.position - transform.position, info.RotateSpeed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
