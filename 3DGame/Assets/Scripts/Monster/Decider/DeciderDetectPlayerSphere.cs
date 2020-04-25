using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeciderDetectPlayerSphere : DeciderBase
{
    // 這個Decider會在"偵測到玩家進入範圍"後回傳true，反之回傳false

    public bool ShowInScene;        // 是否在場景中顯示範圍
    public bool IsRangeStable;      // 範圍是否固定? 若不固定則會跟著怪物移動
    public Vector3 DetectCenter;    // 偵測範圍中心
    public float DetectRadius = 5;  // 偵測範圍半徑

    private Collider[] playerCollider;

    public override bool Decide()
    {
        return Detect();
    }

    /// <summary>
    /// 偵測玩家
    /// </summary>
    private bool Detect()
    {
        if (controller.CurrentStateName != UseStateName)
            return false;

        if(IsRangeStable)
            playerCollider = Physics.OverlapSphere(monsterInfo.InitPosition + DetectCenter, DetectRadius, LayerMask.GetMask("Player"));
        else
            playerCollider = Physics.OverlapSphere(transform.position + DetectCenter, DetectRadius, LayerMask.GetMask("Player"));

        if (playerCollider.Length > 0)
        {
            controller.CurrentTarget = playerCollider[0].gameObject;
            return true;
        }
        return false;
    }

    /// <summary>
    /// 在Scene中繪製對應的範圍
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (!ShowInScene)
            return;

        Gizmos.color = new Color(1, 0, 0, 0.4f);

        if(Application.isPlaying)
            Gizmos.DrawSphere(monsterInfo.InitPosition + DetectCenter, DetectRadius);
        else
            Gizmos.DrawSphere(transform.position + DetectCenter, DetectRadius);
    }
}
