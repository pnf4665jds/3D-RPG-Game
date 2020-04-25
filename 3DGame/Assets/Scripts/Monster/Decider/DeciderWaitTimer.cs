using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeciderWaitTimer : DeciderBase
{
    // 這個Decider會在"在這個狀態中持續WaitTime秒"後回傳true，反之回傳false

    public float WaitTime;

    private float currentTime = 0;

    public override bool Decide()
    {
        if (currentTime >= WaitTime)
        {
            return true;
        }
        else
        {
            currentTime += Time.deltaTime;
            return false;
        }
    }

    public override void Exit()
    {
        base.Exit();
        currentTime = 0;
    }
}
