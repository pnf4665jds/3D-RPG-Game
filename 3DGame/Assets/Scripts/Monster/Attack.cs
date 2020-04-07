using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public MonsterInfo Info;
    public DetectPlayer DetectPlayer;

    void Start()
    {

    }

    void Update()
    {
        if (Info.CurrentState == ActionState.Attack)
        {
            //float colliderWidth = DetectPlayer.PlayerObj.GetComponent<BoxCollider>().size.z / 2;
            float colliderWidth = DetectPlayer.PlayerObj.GetComponent<CapsuleCollider>().radius;

            if (Mathf.Abs(transform.position.z - DetectPlayer.PlayerObj.transform.position.z) > Info.AttackDistance + colliderWidth)
            {
                // 脫離攻擊範圍
                Info.CurrentState = ActionState.Follow;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit");
    }
}
