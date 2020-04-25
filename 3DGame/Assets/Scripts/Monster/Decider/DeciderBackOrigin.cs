using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeciderBackOrigin : DeciderBase
{
    public override bool Decide()
    {
        return transform.position == monsterInfo.InitPosition && monsterInfo.CurrentHP == monsterInfo.MaxHP;
    }
}
