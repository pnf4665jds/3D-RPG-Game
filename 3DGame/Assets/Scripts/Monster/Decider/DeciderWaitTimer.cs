using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeciderWaitTimer : DeciderBase
{
    public float WaitTime;

    private float currentTime = 0;

    public override bool Decide()
    {
        if(currentTime >= WaitTime)
        {
            currentTime = 0;
            return true;
        }
        else
        {
            currentTime += Time.deltaTime;
            return false;
        }
    }
}
