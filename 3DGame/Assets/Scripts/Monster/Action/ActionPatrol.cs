using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPatrol : ActionBase
{
    public bool ShowInScene;    // 是否在場景中顯示範圍
    public float PatrolLength;   // 以初始位置為中心的巡邏範圍

    private Vector3 initPos;
    private Vector3 targetPos;
    private bool startMove = false;

    /// <summary>
    /// 初始化
    /// </summary>
    public override void Init()
    {
        initPos = transform.position;
        StartCoroutine(Calculate());
    }

    /// <summary>
    /// 計算移動位置
    /// </summary>
    /// <returns></returns>
    public IEnumerator Calculate()
    {
        while (true)
        {
            // 先計算隨機的方向
            Vector3 newDir = Quaternion.Euler(0, Random.Range(0, 360), 0) * new Vector3(0, 0, PatrolLength);
            // 再計算最後的移動目的地 = 初始位置 + 移動向量 * 隨機變數
            targetPos = initPos + newDir * Random.Range(0.7f, 1);
            yield return new WaitUntil(() => transform.position == targetPos);
            yield return new WaitForSeconds(2);
        }
    }

    public override void Process()
    {
        Move();
    }

    /// <summary>
    /// 巡邏移動
    /// </summary>
    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, monsterInfo.MoveSpeed * Time.deltaTime);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetPos - transform.position, monsterInfo.RotateSpeed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    public override void Exit()
    {
        StopCoroutine(Calculate());

    }

    /// <summary>
    /// 在Scene中繪製對應的範圍
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (!ShowInScene)
            return;

        Gizmos.color = new Color(1, 1, 0, 0.4f);
        Vector3 target = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0) * new Vector3(0, 0, PatrolLength);
        Gizmos.DrawSphere(transform.position, PatrolLength);
    }
}
