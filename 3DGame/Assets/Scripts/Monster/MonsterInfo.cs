﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfo : MonoBehaviour
{
    public float MaxHP;     // 最大血量
    public float MoveSpeed;         // 移動速度
    public float RotateSpeed;       // 旋轉速度
    public float DeadWaitTime;      // 等待死亡時間
    public int CoinSum;           // 掉落金幣總金額
    public bool IdleWhenAnimation = false;    // 是否要在播放場景動畫時Idle
    public float AttackBuffRate = 1;        // 攻擊強化倍率

    public float CurrentHP { get; private set; }    // 目前血量
    public Vector3 InitPosition { get; private set; }   // 怪物的初始位置
    public Quaternion InitRotation { get; private set; }    // 怪物的初始旋轉
    public Vector3 FieldCenter { get; private set; }    // 活動領域中心座標
    public float FieldRadius { get; private set; }      // 活動領域半徑
    public int MonsterId { get; private set; }      // 怪物編號 

    // 各種狀態
    public bool isGrounded { get; private set; } = false;   // 是否著地
    public bool isDead { get; private set; } = false;   // 是否死亡
    public bool isInvincible { get; set; } = false;     // 是否無敵
    public bool isCollideMonster { get; set; } = false; // 是否撞到其他怪物
    public bool isPause { get; set; } = false;

    [Header("Grounded Detect")]
    public Collider mainCollider;
    public float Offset = 0.2f;    // Collider與地面間的差距

    private Animator animator;
    private float distanceToTheGround = 0.5f;
    private MonsterBlood monsterBlood;
    private RaycastHit hit;

    /// <summary>
    /// 初始化
    /// </summary>
    private void Awake()
    {
        CurrentHP = MaxHP;
        monsterBlood = GetComponentInChildren<MonsterBlood>();
        monsterBlood?.setMaxBlood(MaxHP);
        InitPosition = transform.position;
        InitRotation = transform.rotation;
        animator = GetComponent<Animator>();
        distanceToTheGround = mainCollider.bounds.extents.y + Offset;
        MonsterId = MonsterSystem.instance.AddMonster(this);    // 取得編號
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
        if (isInvincible || isDead)
            return;
        if(animator != null)
            animator.SetTrigger("Damage");  
        CurrentHP = CurrentHP - value;
        monsterBlood?.setCurBlood(CurrentHP > 0 ? CurrentHP : 0);
        // 血量低於0時死亡
        if (CurrentHP <= 0)
        {
            isDead = true;
            StartCoroutine(DeathEvent());
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
    /// 死亡事件
    /// </summary>
    /// <returns></returns>
    private IEnumerator DeathEvent()
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider co in colliders)
            co.enabled = false;

        Rigidbody rigidbody = GetComponentInChildren<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.isKinematic = true;

        // 死亡時關閉ActionController
        MonsterSystem.instance.SetMonsterActionController(MonsterId, false);

        if (animator != null)
            animator.SetBool("Dead", true);
        yield return new WaitForSeconds(DeadWaitTime);
        DropItemSystem.instance.createMoney(transform.position, CoinSum);
        MonsterSystem.instance.RemoveMonster(MonsterId);    // 取消註冊
        //if(gameObject.tag == "Boss")
            //SoundSystem.instance.StopBGM(3);    // 淡出Boss BGM
        Destroy(gameObject);
    }

    /// <summary>
    /// 判斷是否著地
    /// </summary>
    private void CheckGrounded()
    {   
        if (Physics.Raycast(mainCollider.bounds.center, Vector3.down, out hit, distanceToTheGround))
            isGrounded = !hit.collider.isTrigger;
    }

    /// <summary>
    /// 播放場景動畫時讓怪物Idle
    /// </summary>
    /// <returns></returns>
    public IEnumerator IdleOnAnimationState()
    {
        if (!IdleWhenAnimation)
            yield break;

        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        MonsterSystem.instance.SetMonsterActionController(MonsterId, false);
        animator.SetBool("Pause", true);
        yield return new WaitWhile(IsIdleState);
        animator.updateMode = AnimatorUpdateMode.Normal;
        MonsterSystem.instance.SetMonsterActionController(MonsterId, true);
        animator.SetBool("Pause", false);
    }

    /// <summary>
    /// 從GameSystem確認是否需要Idle狀態
    /// </summary>
    /// <returns></returns>
    private bool IsIdleState()
    {
        return GameSystem.instance.isAnimation()
                || GameSystem.instance.isPlayerStory()
                || GameSystem.instance.isPlayerOpenBackPack()
                || GameSystem.instance.isPlayerShopping()
                || GameSystem.instance.isPlayerTalking()
                || GameSystem.instance.isPlayerDead();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Monster")
        {
            isCollideMonster = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            isCollideMonster = false;
        }
    }
}
