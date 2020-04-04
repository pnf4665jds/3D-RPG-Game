using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public DetectPlayer DetectPlayer;
    public MonsterInfo Info;

    private GameObject target;

    private void Start()
    {
    }

    void Update()
    {
        if (DetectPlayer.PlayerObj != null && Info.CurrentState == ActionState.Follow)
        {
            target = DetectPlayer.PlayerObj;
            FollowPlayer();
            // 進入攻擊範圍
            if (Mathf.Abs(transform.position.z - target.transform.position.z) <= Info.AttackDistance)
            {
                Info.CurrentState = ActionState.Attack;
            }
        }
        else if(DetectPlayer.PlayerObj == null && Info.CurrentState == ActionState.Follow)
        {
            Info.CurrentState = ActionState.Patrol;
            GetComponent<Patrol>().Init();
        }
    }

    /// <summary>
    /// 跟隨玩家
    /// </summary>
    private void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Info.MoveSpeed * Time.deltaTime);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, target.transform.position - transform.position, Info.RotateSpeed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
