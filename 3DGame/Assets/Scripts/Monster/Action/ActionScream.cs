using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionScream : ActionBase
{
    public override void Init()
    {
        animator.SetTrigger("Scream");
        SoundSystem.instance.PlaySound2D(ActionSound, SoundDelay);
    }

    public override void Process()
    {
        Vector3 targetPos = GameObject.FindWithTag("Player").transform.position;
        targetPos.y = transform.position.y;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetPos - transform.position, monsterInfo.RotateSpeed * Time.deltaTime, 0.0f);
        newDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    public override void Exit()
    {

    }
}
