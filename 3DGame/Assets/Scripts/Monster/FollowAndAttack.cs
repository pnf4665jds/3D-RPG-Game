using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAndAttack : MonoBehaviour
{
    public DetectPlayer DetectPlayer;
    public float MoveSpeed;
    public float RotateSpeed;
    public float AttackDistance;

    private GameObject target;
    private bool ReadyToAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DetectPlayer.PlayerObj != null)
        {
            target = DetectPlayer.PlayerObj;
            if (Vector3.Distance(transform.position, target.transform.position) <= AttackDistance)
            {
                AttackAnim();
            }
            else
            {
                Follow();
            }
        }
    }

    /// <summary>
    /// 跟隨玩家
    /// </summary>
    private void Follow()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, MoveSpeed * Time.deltaTime);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, target.transform.position - transform.position, RotateSpeed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    /// <summary>
    /// 播放攻擊動畫
    /// </summary>
    private void AttackAnim()
    {
        GetComponent<Animation>().Play("Anim_Attack");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit");
    }
}
