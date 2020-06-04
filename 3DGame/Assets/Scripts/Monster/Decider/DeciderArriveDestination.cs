using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeciderArriveDestination : DeciderBase
{
    // 這個Decider會在"怪物到達目的地"時回傳true，反之回傳false

    public bool isDestSpawnOrigin = false;   // 目的地是怪物生成原點
    public bool shouldConsiderX = true;     // 是否要考慮X軸座標
    public bool shouldConsiderY = true;     // 是否要考慮Y軸座標
    public bool shouldConsiderZ = true;     // 是否要考慮Z軸座標
    public Vector3 customDestination = Vector3.zero;

    private Vector3 destination;

    public override void Init()
    {
        base.Init();
        destination = isDestSpawnOrigin ? monsterInfo.InitPosition : customDestination;
    }

    public override bool Decide()
    {
        if (!shouldConsiderX)
            destination.x = transform.position.x;
        if (!shouldConsiderY)
            destination.y = transform.position.y;
        if (!shouldConsiderZ)
            destination.z = transform.position.z;

        return Vector3.Distance(transform.position, destination) < 0.1f;
    }
}
