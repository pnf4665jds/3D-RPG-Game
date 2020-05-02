using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionFollow : ActionBase
{
    // 這個Action用來讓怪物追玩家

    public float FollowKeepDistance;    // 追隨是否要保持一個距離
    public bool IsFlyMode;      // 是否是飛行狀態

    private GameObject target;

    public override void Init()
    {
        if (!IsFlyMode)
            animator.SetBool("Walk", true);
        else
            animator.SetBool("FlyForward", true);

        target = GameObject.FindWithTag("Player");
    }

    public override void Process()
    {
        if(IsFlyMode || monsterInfo.isGrounded)
            FollowPlayer();
    }

    public override void Exit()
    {
        target = null;
        if(!IsFlyMode)
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

        if (!IsFlyMode)
        {
            if(Vector3.Distance(transform.position, target.transform.position) < FollowKeepDistance)
                return;

            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, monsterInfo.MoveSpeed * Time.deltaTime);
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, target.transform.position - transform.position, monsterInfo.RotateSpeed * Time.deltaTime, 0.0f);
            newDirection.y = 0;
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
        else
        {
            Vector3 targetWithoutY = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            if (Vector3.Distance(transform.position, targetWithoutY) < FollowKeepDistance)
            {
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, targetWithoutY, monsterInfo.MoveSpeed * Time.deltaTime * 1.5f);
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetWithoutY - transform.position, monsterInfo.RotateSpeed * Time.deltaTime, 0.0f);
            newDirection.y = 0;
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
}
