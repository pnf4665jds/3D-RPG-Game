using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeciderUnderHealthPlus : DeciderUnderHealth
{
    public List<float> DeciderHPList;

    private int level = 0;

    public override void Init()
    {
        base.Init();
        DecideHP = DeciderHPList[level];
    }

    public override bool Decide()
    {
        if (monsterInfo.CurrentHP <= DecideHP && level < DeciderHPList.Count)
        {
            level++;
            DecideHP = DeciderHPList[level];
            return true;
        }
        else
        {
            return false;
        }
    }
}
