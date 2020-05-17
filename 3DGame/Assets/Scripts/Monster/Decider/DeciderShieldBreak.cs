using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeciderShieldBreak : DeciderBase
{
    private Shield shield;

    public override void Init()
    {
        base.Init();
        shield = GetComponent<Shield>();
    }

    public override bool Decide()
    {
        return shield.IsShieldBreak;
    }
}
