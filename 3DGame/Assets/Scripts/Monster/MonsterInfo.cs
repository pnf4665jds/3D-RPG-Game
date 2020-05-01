using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfo : MonoBehaviour
{
    public float MaxHP;     // 最大血量
    public float MoveSpeed;         // 移動速度
    public float RotateSpeed;       // 旋轉速度
    public float DeadWaitTime;      // 等待死亡時間
    public int CoinSum;           // 掉落金幣總金額
    public float CurrentHP { get; private set; }    // 目前血量
    public Vector3 InitPosition { get; private set; }   // 怪物的初始位置
    public Quaternion InitRotation { get; private set; }    // 怪物的初始旋轉
    public Vector3 FieldCenter { get; private set; }    // 活動領域中心座標
    public float FieldRadius { get; private set; }      // 活動領域半徑

    private Animator animator;

    private void Start()
    {
        CurrentHP = MaxHP;
        InitPosition = transform.position;
        InitRotation = transform.rotation;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

    }

    /// <summary>
    /// 呼叫後扣血
    /// </summary>
    /// <param name="value"></param>
    public void GetDamage(float value)
    {
        animator.SetTrigger("Damage");  
        CurrentHP = CurrentHP - value;

        // 血量低於0時死亡
        if (CurrentHP <= 0)
        {
            StartCoroutine(DeathAnim());
        }
    }

    /// <summary>
    /// 呼叫後回血
    /// </summary>
    /// <param name="value"></param>
    public void GetHeal(float value)
    {
        CurrentHP = CurrentHP + value;
        Debug.Log(CurrentHP);
        if (CurrentHP > MaxHP)
            CurrentHP = MaxHP;
    }

    /// <summary>
    /// 設定活動領域的中心點跟半徑
    /// </summary>
    public void SetField(Vector3 center, float radius)
    {
        FieldCenter = center;
        FieldRadius = radius;
    }

    /// <summary>
    /// 死亡動畫
    /// </summary>
    /// <returns></returns>
    private IEnumerator DeathAnim()
    {
        Rigidbody rigidbody = GetComponentInChildren<Rigidbody>();
        rigidbody.isKinematic = true;

        animator.SetBool("Dead", true);
        yield return new WaitForSeconds(DeadWaitTime);
        DropItemSystem.instance.createMoney(transform.position, CoinSum);
        Destroy(gameObject);
    }
}
