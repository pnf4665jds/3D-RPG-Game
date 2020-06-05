using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionFlyInCircle : ActionBase
{
    [Header("Fly")]
    public float MoveSpeedRate = 1;
    public float RotateSpeedRate = 1;

    private Vector3 targetPos;
    private bool startMove = false;
    private int clockWise = 1;

    public override void Init()
    {
        StartCoroutine(Calculate());
        animator.SetBool("FlyForward", true);
        rigid.useGravity = false;
        SoundSystem.instance.PlaySound(Source, ActionSound, Volume, SoundDelay, true);
        clockWise = Random.Range(0, 2) == 0 ? 1 : -1;
    }

    public override void Process()
    {
        if(startMove)
            Move();
    }

    public override void Exit()
    {
        StopCoroutine(Calculate());
        //rigid.useGravity = true;
    }

    /// <summary>
    /// 計算移動位置
    /// </summary>
    /// <returns></returns>
    private IEnumerator Calculate()
    {
        yield return null;
        float val = 0;
        while (true)
        {
            val += Time.deltaTime * clockWise;
            if (val >= 1 || val <= -1)
                val = 0;
            float theta = val * 2 * Mathf.PI;
            float len = monsterInfo.FieldRadius * 0.9f;
            targetPos = new Vector3(monsterInfo.FieldCenter.x + len * Mathf.Cos(theta), transform.position.y, monsterInfo.FieldCenter.z + len * Mathf.Sin(theta));
            startMove = true;
            yield return new WaitUntil(() => Vector3.Distance(transform.position, targetPos) < 0.1f);
            startMove = false;
        }
    }

    /// <summary>
    /// 巡邏移動
    /// </summary>
    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, monsterInfo.MoveSpeed * Time.deltaTime * MoveSpeedRate);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetPos - transform.position, monsterInfo.RotateSpeed * Time.deltaTime * RotateSpeedRate, 0.0f);
        newDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
