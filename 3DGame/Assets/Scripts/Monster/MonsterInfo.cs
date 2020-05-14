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
    public bool isDead { get; private set; } = false;   // 是否死亡
    public bool isInvincible { get; set; } = false;     // 是否無敵

    [Header("Grounded Detect")]
    public Collider mainCollider;
    public float Offset = 0.2f;    // Collider與地面間的差距

    private Animator animator;
    private float distanceToTheGround = 0.5f;
    private MonsterBlood monsterBlood;

    private void Awake()
    {
        CurrentHP = MaxHP;
        monsterBlood = GetComponentInChildren<MonsterBlood>();
        monsterBlood?.setMaxBlood(MaxHP);
        InitPosition = transform.position;
        InitRotation = transform.rotation;
        animator = GetComponent<Animator>();
        distanceToTheGround = mainCollider.bounds.extents.y + Offset;
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
        if (isInvincible)
            return;
        if(animator != null)
            animator.SetTrigger("Damage");  
        CurrentHP = CurrentHP - value;
        monsterBlood?.setCurBlood(CurrentHP > 0 ? CurrentHP : 0);
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
        monsterBlood?.setCurBlood(CurrentHP < MaxHP ? CurrentHP : MaxHP);
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

        if (animator != null)
            animator.SetBool("Dead", true);
        yield return new WaitForSeconds(DeadWaitTime);
        DropItemSystem.instance.createMoney(transform.position, CoinSum);

        // 如果是BOSS死亡，則傳遞死亡訊息給Potal
        if(gameObject.tag == "Boss")
        {
            FindObjectOfType<Portal>().SetCondition(PortalCondition.BossDead, true);
        }
        Destroy(gameObject);
    }

    /// <summary>
    /// 判斷是否著地
    /// </summary>
    private void CheckGrounded()
    {
        isGrounded = Physics.Raycast(mainCollider.bounds.center, Vector3.down, distanceToTheGround);
    }
}
