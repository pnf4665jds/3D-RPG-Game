using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionBase : MonoBehaviour
{
    // 子類別必須實作以下三個函式
    public abstract void Init();
    public abstract void Process();        
    public abstract void Exit();

    public string UseStateName;     // 這個Action使用在哪個state

    protected ActionController controller { get; set; }
    protected Animator animator { get; set; }
    protected MonsterInfo monsterInfo { get; set; }
    protected Rigidbody rigid { get; set; }

    private void Awake()
    {
        controller = GetComponent<ActionController>();
        animator = GetComponent<Animator>();
        monsterInfo = GetComponent<MonsterInfo>();
        rigid = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (controller.CurrentStateName != UseStateName)
            return;

        DoOnTriggerEnter(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (controller.CurrentStateName != UseStateName)
            return;

        DoOnCollisionEnter(collision);
    }

    /// <summary>
    /// 碰到Trigger時做什麼
    /// </summary>
    protected virtual void DoOnTriggerEnter(Collider other)
    {
        // 讓子類別繼承+實作
    }

    /// <summary>
    /// 碰到Collider時做什麼
    /// </summary>
    protected virtual void DoOnCollisionEnter(Collision collision)
    {
        // 讓子類別繼承+實作
    }
}
