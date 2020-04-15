using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfo : MonoBehaviour
{
    public float MaxHP;
    public float CurrentHP { get; private set; }
    public float MoveSpeed;
    public float RotateSpeed;
    public float AttackDistance;
    //public ActionState CurrentState { get; set; }

    private Animator animator;

    private void Start()
    {
        //CurrentState = ActionState.Patrol;
        CurrentHP = MaxHP;
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
            //CurrentState = ActionState.Dead;
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
