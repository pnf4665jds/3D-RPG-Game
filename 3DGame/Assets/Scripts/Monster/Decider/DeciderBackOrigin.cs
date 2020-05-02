using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeciderBackOrigin : DeciderBase
{
    // 這個Decider會在"怪物回到初始位置並且血量回滿"時回傳true，反之回傳false

    private Vector3 origin;

    public override bool Decide()
    {
        origin = monsterInfo.InitPosition;
        origin.y = transform.position.y;
        bool close = Vector3.Distance(transform.position, origin) < 0.1f;
        return close && monsterInfo.CurrentHP == monsterInfo.MaxHP;
    }
}
