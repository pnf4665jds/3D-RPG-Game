using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionScream : ActionBase
{
    private float timer;

    public override void Init()
    {
        timer = 0;
        animator.SetTrigger("Scream");
        SoundSystem.instance.PlaySound(Source, ActionSound, Volume, SoundDelay, false);
    }

    public override void Process()
    {
        if (timer < SoundDelay)
        {
            timer += Time.deltaTime;
            Vector3 targetPos = GameObject.FindWithTag("Player").transform.position;
            targetPos.y = transform.position.y;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetPos - transform.position, monsterInfo.RotateSpeed * Time.deltaTime, 0.0f);
            newDirection.y = 0;
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    public override void Exit()
    {

    }
}
