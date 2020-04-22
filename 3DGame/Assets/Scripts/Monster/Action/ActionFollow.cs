using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionFollow : ActionBase
{
    public float FollowKeepDistance;
    public bool FlyMode;

    private MonsterInfo info;
    private GameObject target;

    public override void Init()
    {
        info = GetComponent<MonsterInfo>();
        if (!FlyMode)
            animator.SetBool("Walk", true);
        else
            animator.SetBool("FlyForward", true);
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
        if(!FlyMode)
            animator.SetBool("Walk", false);
        else
            animator.SetBool("FlyForward", false);
    }

    /// <summary>
    /// 跟隨玩家
    /// </summary>
    private void FollowPlayer()
    {
        if (target == null)
            return;

        if (!FlyMode)
        {
            if(Vector3.Distance(transform.position, target.transform.position) < FollowKeepDistance)
                return;

            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, info.MoveSpeed * Time.deltaTime);
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, target.transform.position - transform.position, info.RotateSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
        else
        {
            Vector3 targetWithoutY = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            if (Vector3.Distance(transform.position, targetWithoutY) < FollowKeepDistance)
            {

                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, targetWithoutY, info.MoveSpeed * Time.deltaTime * 1.5f);
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetWithoutY - transform.position, info.RotateSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
}
