using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPatrol : ActionBase
{
    // 這個Action用來讓怪物在活動領域內巡邏

    private Vector3 initPos;
    private Vector3 targetPos;
    private bool startMove = false;
    private bool reCalculate = false;

    /// <summary>
    /// 初始化
    /// </summary>
    public override void Init()
    {
        //initPos = transform.position;
        startMove = false;
        StartCoroutine(Calculate());
    }

    /// <summary>
    /// 計算移動位置
    /// </summary>
    /// <returns></returns>
    public IEnumerator Calculate()
    {
        // 等待怪物著地後才開始移動
        yield return new WaitUntil(() => monsterInfo.isGrounded);
        while (true)
        {
            float theta = Random.Range(0f, 1f) * 2 * Mathf.PI;
            float len = Random.Range(monsterInfo.FieldRadius * 0.2f, monsterInfo.FieldRadius);
            //centerX + len * cos(theta), centerZ + len * sin(theta)
            targetPos = new Vector3(monsterInfo.FieldCenter.x + len * Mathf.Cos(theta), transform.position.y, monsterInfo.FieldCenter.z + len * Mathf.Sin(theta));
            startMove = true;
            animator.SetBool("Walk", true);
            reCalculate = false;
            yield return CheckArrive();
            if (!reCalculate)
            {
                startMove = false;
                animator.SetBool("Walk", false);
                yield return new WaitForSeconds(2);
            }
        }
    }

    public override void Process()
    {
        if(monsterInfo.isCollideMonster)
            Debug.Log("Co");
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
        animator.SetBool("Walk", false);
        StopCoroutine(Calculate());
    }

    /// <summary>
    /// 檢查是否抵達目的地或者中途與其他怪物碰撞
    /// </summary>
    /// <returns></returns>
    private IEnumerator CheckArrive()
    {
        while(Vector3.Distance(transform.position, targetPos) >= 0.5f)
        {
            if (monsterInfo.isCollideMonster)
            {
                reCalculate = true;
                yield break;
            }
            yield return null;
        }
    }
}
