using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHealAndBack : ActionBase
{
    // 這個Action用來讓怪物回到初始位置並且回滿血

    private Vector3 initPosition;
    private Quaternion initRotation;

    public override void Init()
    {
        animator.SetBool("Walk", true);
        initPosition = monsterInfo.InitPosition;
        initRotation = monsterInfo.InitRotation;
        StartCoroutine(Heal());
    }

    public override void Process()
    {
        MoveToOrigin();
    }

    public override void Exit()
    {
        animator.SetBool("Walk", false);
        StartCoroutine(RotateToOrigin());
        controller.InitDecider();
    }

    /// <summary>
    /// 移動到初始位置
    /// </summary>
    private void MoveToOrigin()
    {
        transform.position = Vector3.MoveTowards(transform.position, initPosition, monsterInfo.MoveSpeed * Time.deltaTime);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, initPosition - transform.position, monsterInfo.RotateSpeed * Time.deltaTime, 0.0f);
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
            monsterInfo.GetHeal(monsterInfo.MaxHP * 0.05f);
            yield return new WaitForSeconds(0.5f);
        }
    }

    /// <summary>
    /// 回到初始旋轉
    /// </summary>
    /// <returns></returns>
    private IEnumerator RotateToOrigin()
    {
        Quaternion from = transform.rotation;
        Quaternion to = initRotation;
        int elapsed = 0;

        while(elapsed < 100)
        {
            transform.rotation = Quaternion.Slerp(from, to, 0.01f * elapsed);
            elapsed += 1;
            yield return null;
        }
    }
}
