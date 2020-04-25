using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeciderBackOrigin : DeciderBase
{
    // 這個Decider會在"怪物回到初始位置並且血量回滿"時回傳true，反之回傳false

    public override bool Decide()
    {
        return transform.position == monsterInfo.InitPosition && monsterInfo.CurrentHP == monsterInfo.MaxHP;
    }
}
