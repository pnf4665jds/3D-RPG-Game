using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHealAndBack : ActionBase
{
    // 這個Action用來讓怪物回到初始位置並且回滿血

    public bool ShouldConsiderY;    // 是否要考慮Y軸座標

    private Vector3 targetPos;

    public override void Init()
    {
        animator.SetBool("Walk", true);
        //initPosition = monsterInfo.InitPosition;
        targetPos = monsterInfo.InitPosition;
        StartCoroutine(Heal());
    }

    public override void Process()
    {
        MoveToOrigin();
    }

    public override void Exit()
    {
        animator.SetBool("Walk", false);
        controller.InitAllDecider();   // 初始化所有Decider
    }

    /// <summary>
    /// 移動到初始位置
    /// </summary>
    private void MoveToOrigin()
    {
        if (!ShouldConsiderY)
            targetPos.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, monsterInfo.MoveSpeed * Time.deltaTime);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetPos - transform.position, monsterInfo.RotateSpeed * Time.deltaTime, 0.0f);
        newDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    /// <summary>
    /// 持續回血
    /// </summary>
    /// <returns></returns>
    private IEnumerator Heal()
    {
        while(monsterInfo.CurrentHP < monsterInfo.MaxHP)
        {
            monsterInfo.GetHeal(monsterInfo.MaxHP * 0.1f);
            yield return new WaitForSeconds(0.5f);
        }
    }

    /// <summary>
    /// 回到初始旋轉
    /// </summary>
    /// <returns></returns>
    /*private IEnumerator RotateToOrigin()
    {
        Quaternion from = transform.rotation;
        Quaternion to = initRotation;
        int elapsed = 0;

        while(elapsed < 50)
        {
            transform.rotation = Quaternion.Slerp(from, to, 0.02f * elapsed);
            elapsed += 1;
            yield return null;
        }
    }*/
}
