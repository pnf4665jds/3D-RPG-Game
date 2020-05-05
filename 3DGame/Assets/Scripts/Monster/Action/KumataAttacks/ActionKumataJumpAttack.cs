using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionKumataJumpAttack : ActionBase
{
    // 這個Action用來讓Kumata進行跳躍衝擊(跳到傷害範圍中心)

    public float Damage;    // 傷害
    public string TriggerName;  // 要呼叫的Trigger名稱

    [Header("Damage Area")]
    public bool ShowInScene;        // 是否在場景中顯示範圍
    public Vector3 DamageAreaCenter;    // 傷害範圍中心
    public Vector3 DamageAreaSize = new Vector3(1, 1, 1);      // 傷害範圍大小
    public float AreaDelayTime;
    public float AreaKeepTime;      // 傷害範圍持續時間

    [Header("Jumping")]
    public float DelayBeforeJump;
    public float JumpTime;
    public float JumpHeight;

    [Header("Effect")]
    public GameObject Effect;
    public float EffectDelay;

    private Vector3 jumpDestination;
    private Vector3 jumpStartPos;
    private float timer;

    public override void Init()
    {
        animator.SetTrigger(TriggerName);
        timer = 0;
        jumpStartPos = gameObject.transform.position;
        jumpDestination = gameObject.transform.position + GetCenterVector(DamageAreaCenter) * 0.4f;
        GetComponent<Rigidbody>().useGravity = false;
        StartCoroutine(Jump());
        StartCoroutine(SetDamageArea());
        StartCoroutine(StartEffect());
    }

    public override void Process()
    {
        
    }

    public override void Exit()
    {
        GetComponent<Rigidbody>().useGravity = true;
    }

    public IEnumerator Jump()
    {
        yield return new WaitForSeconds(DelayBeforeJump);
        while (timer <= JumpTime)
        {
            timer += Time.deltaTime;
            transform.position = MathParabola.Parabola(jumpStartPos, jumpDestination, JumpHeight, timer / JumpTime);
            yield return null;
        }
    }

    /// <summary>
    /// 設置傷害區域
    /// </summary>
    /// <returns></returns>
    public IEnumerator SetDamageArea()
    {
        yield return new WaitForSeconds(AreaDelayTime);
        GameObject area = new GameObject();
        DamageAreaCreator creator = area.AddComponent<DamageAreaCreator>();
        creator.CreateCubeArea(jumpStartPos + GetCenterVector(DamageAreaCenter), transform.rotation, DamageAreaSize, Damage, AreaKeepTime);
    }

    /// <summary>
    /// 設置特效
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartEffect()
    {
        yield return new WaitForSeconds(EffectDelay);
        GameObject effect = Instantiate(Effect, transform);
        effect.transform.position = transform.position + GetCenterVector(new Vector3(0, -3, 11));
        Destroy(effect, 8);
        yield return new WaitForSeconds(1);
        effect.transform.position = transform.position + GetCenterVector(new Vector3(0, 0, 5));
    }

    /// <summary>
    /// 取得旋轉後向量
    /// </summary>
    /// <returns></returns>
    public Vector3 GetCenterVector(Vector3 v)
    {
        return Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * v;
    }

    /// <summary>
    /// 在Scene中繪製對應的範圍
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (!ShowInScene)
            return;

        Vector3 newDir = GetCenterVector(DamageAreaCenter);
        Gizmos.matrix = Matrix4x4.TRS(transform.position + newDir, transform.rotation, DamageAreaSize * 2);
        Gizmos.color = new Color(1, 0, 0, 0.4f);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);

    }
}
