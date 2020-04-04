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
    public Animation Anim;

    private void Start()
    {
        CurrentState = ActionState.Patrol;
    }

    private void Update()
    {
        if(CurrentState == ActionState.Attack)
        {
            Anim.Play("Anim_Attack");   
        }
        else if (CurrentState == ActionState.Follow || CurrentState == ActionState.Patrol)
        {
            Anim.Play("Anim_Run");
        }
        else
        {
            Anim.Play("Anim_Idle");
        }
    }
}

public enum ActionState
{
    Follow,
    Patrol,
    Attack
}
