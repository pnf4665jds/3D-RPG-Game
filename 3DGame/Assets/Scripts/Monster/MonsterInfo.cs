using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfo : MonoBehaviour
{
    public float HP;
    public float MoveSpeed;
    public float RotateSpeed;
    public float AttackDistance;
    public ActionState CurrentState { get; set; }
    public Animator animator;

    private void Start()
    {
        CurrentState = ActionState.Patrol;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GetDamage(100);
        }
    }

    /// <summary>
    /// 呼叫後扣血
    /// </summary>
    /// <param name="value"></param>
    public void GetDamage(float value)
    {
        animator.SetTrigger("Damage");  
        HP = HP - value;

        // 血量低於0時死亡
        if (HP <= 0)
        {
            CurrentState = ActionState.Dead;
            StartCoroutine(DeathAnim());
        }
    }

    /// <summary>
    /// 死亡動畫
    /// </summary>
    /// <returns></returns>
    private IEnumerator DeathAnim()
    {
        GetComponent<Collider>().enabled = false;
        animator.SetBool("Dead", true);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}

public enum ActionState
{
    Follow,
    Patrol,
    Attack,
    Dead
}
