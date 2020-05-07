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
    public bool isGrounded { get; private set; } = false;   // 是否著地
    public bool isDead { get; private set; } = false;

    private Animator animator;
    private Collider mainCollider;

    private void Awake()
    {
        CurrentHP = MaxHP;
        InitPosition = transform.position;
        InitRotation = transform.rotation;
        animator = GetComponent<Animator>();
        Collider[] c = GetComponentsInChildren<Collider>();
        foreach(Collider cc in c)
        {
            if(cc.isTrigger == false)
            {
                mainCollider = cc;
                break;
            }
        }
        
    }

    private void Update()
    {
        CheckGrounded();
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
            isDead = true;
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
    /// 取得目前位置與領域中心點的距離
    /// </summary>
    /// <returns></returns>
    public float GetDisToFieldCenter()
    {
        return Vector3.Distance(transform.position, FieldCenter);
    }

    /// <summary>
    /// 取得根據角色旋轉所得的新向量
    /// </summary>
    /// <returns></returns>
    public Vector3 GetCenterVector(Vector3 v)
    {
        return Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * v;
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

    private void CheckGrounded()
    {
        float DisstanceToTheGround = mainCollider.bounds.extents.y;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, DisstanceToTheGround + 0.1f);
    }
    /// <summary>
    /// 判定是否著地
    /// </summary>
    /// <param name="collision"></param>
    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            isGrounded = true;
        }
    }

    /// <summary>
    /// 判定是否離開地面
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            isGrounded = false;
        }
    }*/
}
