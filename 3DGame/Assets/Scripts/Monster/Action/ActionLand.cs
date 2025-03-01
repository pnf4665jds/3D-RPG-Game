﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionLand : ActionBase
{
    // 這個Action用來讓怪物進行著地動作

    [Header("Land")]
    public bool LandAtFieldCenter = false;
    public float InitLandSpeed = 0;
    public GameObject LandEffect;

    public override void Init()
    {
        if (LandAtFieldCenter)
            StartCoroutine(MoveToCenter());
        else
        {
            animator.SetBool("FlyForward", false);
            animator.SetTrigger("Land");
            rigid.useGravity = true;
            StartCoroutine(SetEffect());
        }
    }

    public override void Process()
    {
        
    }

    public override void Exit()
    {
        
    }

    private IEnumerator MoveToCenter()
    {
        Vector3 targetPos = monsterInfo.FieldCenter;
        targetPos.y = transform.position.y;
        while(Vector3.Distance(transform.position, targetPos) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, monsterInfo.MoveSpeed * Time.deltaTime * 1.5f);
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetPos - transform.position, monsterInfo.RotateSpeed * Time.deltaTime, 0.0f);
            newDirection.y = 0;
            transform.rotation = Quaternion.LookRotation(newDirection);
            yield return null;
        }
        StartCoroutine(SetEffect());
        animator.SetBool("FlyForward", false);
        animator.SetTrigger("Land");
        yield return new WaitForSeconds(1);
        rigid.useGravity = true;
        rigid.velocity += new Vector3(0, -InitLandSpeed, 0);
    }

    private IEnumerator SetEffect()
    {
        yield return new WaitUntil(() => monsterInfo.isGrounded);
        SoundSystem.instance.PlaySound(Source, ActionSound, Volume, SoundDelay, false);
        GameObject obj = Instantiate(LandEffect, transform.position, Quaternion.identity);
        Destroy(obj, 3);
    }
}
