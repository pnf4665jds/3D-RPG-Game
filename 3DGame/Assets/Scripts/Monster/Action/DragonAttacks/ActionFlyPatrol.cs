using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionFlyPatrol : ActionBase
{
    // 這個Action用來讓怪物在飛行狀態下巡邏

    private Vector3 initPos;
    private Vector3 targetPos;
    private bool startMove = false;

    /// <summary>
    /// 初始化
    /// </summary>
    public override void Init()
    {
        StartCoroutine(Calculate());
        animator.SetBool("FlyForward", true);
    }

    /// <summary>
    /// 計算移動位置
    /// </summary>
    /// <returns></returns>
    public IEnumerator Calculate()
    {
        yield return null;
        while (true)
        {
            float theta = Random.Range(0f, 1f) * 2 * Mathf.PI;
            float len = Random.Range(monsterInfo.FieldRadius * 0.2f, monsterInfo.FieldRadius);
            //centerX + len * cos(theta), centerZ + len * sin(theta)
            targetPos = new Vector3(monsterInfo.FieldCenter.x + len * Mathf.Cos(theta), transform.position.y, monsterInfo.FieldCenter.z + len * Mathf.Sin(theta));
            //targetPos = initPos + newDir * finalLength * 0.9f;
            startMove = true;
            yield return new WaitUntil(() => Vector3.Distance(transform.position, targetPos) < 0.1f);
            startMove = false;
        }
    }

    public override void Process()
    {
        if(startMove)
            Move();
    }

    /// <summary>
    /// 巡邏移動
    /// </summary>
    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, monsterInfo.MoveSpeed * Time.deltaTime);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetPos - transform.position, monsterInfo.RotateSpeed * Time.deltaTime, 0.0f);
        newDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    public override void Exit()
    {
        StopCoroutine(Calculate());
    }
}
