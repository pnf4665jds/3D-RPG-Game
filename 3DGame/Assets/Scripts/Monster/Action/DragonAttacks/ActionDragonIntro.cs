using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDragonIntro : ActionBase
{
    public override void Init()
    {
        StartCoroutine(Intro());
    }

    public override void Process()
    {

    }

    public override void Exit()
    {

    }

    private IEnumerator Intro()
    {
        yield return null;
    }
}
