using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionFollow : ActionBase
{
    private MonsterInfo info;
    private GameObject target;

    public override void Init()
    {
        info = GetComponent<MonsterInfo>();
        animator.SetBool("Walk", true);
    }

    public override void Process()
    {
        if (controller.CurrentTarget)
            target = controller.CurrentTarget;

        FollowPlayer();
    }

    public override void Exit()
    {
        target = null;
        animator.SetBool("Walk", false);
    }

    /// <summary>
    /// 跟隨玩家
    /// </summary>
    private void FollowPlayer()
    {
        if (target == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, info.MoveSpeed * Time.deltaTime);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, target.transform.position - transform.position, info.RotateSpeed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
