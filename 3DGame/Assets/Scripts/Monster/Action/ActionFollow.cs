using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActionFollow : ActionBase
{
    // 這個Action用來讓怪物追玩家

    public bool IsFieldLimited = true; // 是否會被領域控制，若否請使用NavMeshAgent
    public bool IsFlyMode;      // 是否是飛行狀態
    public float FlySpeedUp = 1f;

    private GameObject target;
    private NavMeshAgent agent;

    public override void Init()
    {
        if (!IsFlyMode)
            animator.SetBool("Walk", true);
        else
            animator.SetBool("FlyForward", true);

        target = GameObject.FindWithTag("Player");
        if (!IsFieldLimited)
        {
            agent = GetComponent<NavMeshAgent>();
            agent.enabled = true;
            agent.speed = monsterInfo.MoveSpeed;
        }
    }

    public override void Process()
    {
        if(IsFlyMode || monsterInfo.isGrounded)
            FollowPlayer();
    }

    public override void Exit()
    {
        target = null;
        if (!IsFieldLimited)
        {
            agent.enabled = false;
        }
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

        if (IsFieldLimited)
        {
            Vector3 finalPos = IsFlyMode ? new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z) : target.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, finalPos, monsterInfo.MoveSpeed * Time.deltaTime * FlySpeedUp);
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, finalPos - transform.position, monsterInfo.RotateSpeed * Time.deltaTime, 0.0f);
            newDirection.y = 0;
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
        else
        {
            agent.SetDestination(target.transform.position);
        }
    }
}
