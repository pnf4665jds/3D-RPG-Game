using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeciderExitField : DeciderBase
{
    // 這個Decider會在"怪物脫離活動領域"時回傳true，反之回傳false

    private Vector3 fieldCenter;
    private float fieldRadius;

    public override void Init()
    {
        base.Init();
        fieldCenter = monsterInfo.FieldCenter;
        fieldRadius = monsterInfo.FieldRadius;
    }

    public override bool Decide()
    {
        return Vector3.Distance(transform.position, fieldCenter) > fieldRadius;
    }
}
