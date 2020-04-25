using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeciderDetectPlayerRect : DeciderBase
{
    // 這個Decider會在"偵測到玩家進入範圍"後回傳true，反之回傳false

    public bool ShowInScene;        // 是否在場景中顯示範圍
    public bool IsRangeStable;      // 範圍是否固定? 若不固定則會跟著怪物移動
    public Vector3 DetectCenter;    // 偵測範圍中心
    public Vector3 DetectSize = new Vector3(1, 1, 1);      // 偵測範圍大小
    public float MinDistanceLimit;      // 最小距離限制
    public float MaxDistanceLimit;      // 最大距離限制

    private Collider[] playerCollider;

    public override bool Decide()
    {
        return Detect();
    }

    /// <summary>
    /// 取得偵測框的中心
    /// </summary>
    /// <returns></returns>
    public Vector3 GetCenterVector()
    {
        return Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * DetectCenter;
    }

    /// <summary>
    /// 偵測玩家
    /// </summary>
    private bool Detect()
    {
        if (controller.CurrentStateName != UseStateName)
            return false;

        if (MinDistanceLimit != 0 && controller.CurrentTarget && Vector3.Distance(controller.CurrentTarget.transform.position, transform.position) < MinDistanceLimit)
            return false;

        if (MaxDistanceLimit != 0 && controller.CurrentTarget && Vector3.Distance(controller.CurrentTarget.transform.position, transform.position) > MaxDistanceLimit)
            return false;

        if (IsRangeStable)
        {
            playerCollider = Physics.OverlapBox(monsterInfo.InitPosition + DetectCenter, DetectSize, transform.rotation, LayerMask.GetMask("Player"));
        }
        else
        {
            Vector3 newDir = GetCenterVector();
            playerCollider = Physics.OverlapBox(transform.position + newDir, DetectSize, transform.rotation, LayerMask.GetMask("Player"));
        }

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

        if (IsRangeStable)
        {
            monsterInfo = GetComponent<MonsterInfo>();
            Gizmos.color = new Color(1, 0, 0, 0.4f);
            if (Application.isPlaying)
                Gizmos.DrawWireCube(monsterInfo.InitPosition + DetectCenter, DetectSize * 2);
            else
                Gizmos.DrawWireCube(transform.position + DetectCenter, DetectSize * 2);
        }
        else
        {
            Vector3 newDir = GetCenterVector();
            Gizmos.matrix = Matrix4x4.TRS(transform.position + newDir, transform.rotation, DetectSize * 2);
            Gizmos.color = new Color(1, 0, 0, 0.4f);
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        }
    }
}
