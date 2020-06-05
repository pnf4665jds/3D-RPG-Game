using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeciderArriveDestination : DeciderBase
{
    // 這個Decider會在"怪物到達目的地"時回傳true，反之回傳false

    public bool IsDestSpawnOrigin = false;   // 目的地是怪物生成原點
    public bool ShouldConsiderX = true;     // 是否要考慮X軸座標
    public bool ShouldConsiderY = true;     // 是否要考慮Y軸座標
    public bool ShouldConsiderZ = true;     // 是否要考慮Z軸座標
    public Vector3 CustomDestination = Vector3.zero;

    private Vector3 destination;

    public override void Init()
    {
        base.Init();
        destination = IsDestSpawnOrigin ? monsterInfo.InitPosition : CustomDestination;
    }

    public override bool Decide()
    {
        return (Mathf.Abs(transform.position.x - destination.x) < 0.1f || !ShouldConsiderX)
                && (Mathf.Abs(transform.position.y - destination.y) < 0.1f || !ShouldConsiderY)
                && (Mathf.Abs(transform.position.z - destination.z) < 0.1f || !ShouldConsiderZ);
    }
}
