using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMoveToKeepPos : ActionBase
{
    // 這個Action用來讓怪物移動到與目標保持一定距離的地方

    public bool IsFlyMode;            // 是否是飛行狀態
    public float KeepDistance;      // 保持的距離

    private GameObject player;
    private Vector3 targetWithoutY;
    private bool readyToEval;       // 是否可以開始計算移動的目的地了

    public override void Init()
    {
        if (!IsFlyMode)
            animator.SetBool("Walk", true);
        else
            animator.SetBool("FlyForward", true);

        readyToEval = false;
    }

    public override void Process()
    {
        if (controller.CurrentTarget && !readyToEval)
        {
            player = controller.CurrentTarget;
            readyToEval = true;
            targetWithoutY = GetTargetPos();
        }

        if (Vector3.Distance(transform.position, targetWithoutY) > 0.01f)
        {
            MoveToTarget();
        }
        else
        {
            Vector3 playerPosWithoutY = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, playerPosWithoutY - transform.position, monsterInfo.RotateSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    public override void Exit()
    {
        player = null;
        if (!IsFlyMode)
            animator.SetBool("Walk", false);
        else
            animator.SetBool("FlyForward", false);
    }

    /// <summary>
    /// 前往目的地
    /// </summary>
    private void MoveToTarget()
    {
        if (player == null)
            return;

        if (!IsFlyMode)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWithoutY, monsterInfo.MoveSpeed * Time.deltaTime);
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetWithoutY - transform.position, monsterInfo.RotateSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWithoutY, monsterInfo.MoveSpeed * Time.deltaTime * 1.5f);
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetWithoutY - transform.position, monsterInfo.RotateSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    /// <summary>
    /// 取得目的地位置
    /// </summary>
    /// <returns></returns>
    private Vector3 GetTargetPos()
    {
        Vector3 playerPosWithoutY = player.transform.position;
        playerPosWithoutY.y = transform.position.y;
        Vector3 initPosWithoutY = monsterInfo.InitPosition;
        initPosWithoutY.y = transform.position.y;
        float rate = KeepDistance / Vector3.Distance(initPosWithoutY, playerPosWithoutY);

        return playerPosWithoutY + (initPosWithoutY - playerPosWithoutY) * rate;
    }
}
