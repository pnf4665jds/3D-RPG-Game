using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeciderUnderHealth : DeciderBase
{
    // 這個Decider會在"血量低於DecideHP"後回傳true，反之回傳false

    // 血量低於多少時轉移狀態
    public float DecideHP;

    public override bool Decide()
    {
        return monsterInfo.CurrentHP <= DecideHP;
    }
}
