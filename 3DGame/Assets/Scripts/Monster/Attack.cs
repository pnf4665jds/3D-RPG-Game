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
        if(Info.CurrentState == ActionState.Attack && Mathf.Abs(transform.position.z - DetectPlayer.PlayerObj.transform.position.z) > Info.AttackDistance)
        {
            Info.CurrentState = ActionState.Follow;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit");
    }
}
