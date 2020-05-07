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
            // 先計算隨機的方向向量
            Vector3 newDir = Quaternion.Euler(0, Random.Range(0, 360), 0) * new Vector3(0, 0, 1);
            // 計算往領域中心的向量
            Vector3 DirToInit = monsterInfo.FieldCenter - transform.position;
            DirToInit.y = 0;
            // 取兩向量夾角
            float angle = Mathf.Deg2Rad * Vector3.Angle(newDir, DirToInit);
            // 計算最終距離
            float a = monsterInfo.GetDisToFieldCenter();
            float b = monsterInfo.FieldRadius;
            float c = Mathf.Pow(2 * a * Mathf.Cos(angle), 2) - 4 * (Mathf.Pow(a, 2) - Mathf.Pow(b, 2));
            float finalLength = (2 * a * Mathf.Cos(angle) + Mathf.Sqrt(c)) / 2;
            // 再計算最後的移動目的地 = 初始位置 + 移動向量 * 隨機變數
            initPos = transform.position;
            targetPos = initPos + newDir * finalLength * 0.9f;
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
