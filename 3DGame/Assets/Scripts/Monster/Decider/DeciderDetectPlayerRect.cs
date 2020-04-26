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
    public float StableRotateY = 0;       // 範圍固定時的Y軸旋轉

    private Collider[] playerCollider;
    private Quaternion stableRotation;

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

    public Vector3 GetStableVector()
    {
        return Quaternion.AngleAxis(StableRotateY, Vector3.up) * DetectCenter;
    }

    /// <summary>
    /// 偵測玩家
    /// </summary>
    private bool Detect()
    {
        if (controller.CurrentStateName != UseStateName)
            return false;

        if (IsRangeStable)
        {
            stableRotation = Quaternion.Euler(0, StableRotateY, 0);
            playerCollider = Physics.OverlapBox(monsterInfo.InitPosition + DetectCenter, DetectSize, stableRotation, LayerMask.GetMask("Player"));
        }
        else
        {
            Vector3 newDir = GetCenterVector();
            playerCollider = Physics.OverlapBox(transform.position + newDir, DetectSize, transform.rotation, LayerMask.GetMask("Player"));
        }

        if (playerCollider.Length > 0)
            return true;
        
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
            Vector3 newDir = GetStableVector();
            Gizmos.color = new Color(1, 0, 0, 0.4f);

            if (Application.isPlaying)
            {
                stableRotation = Quaternion.Euler(0, StableRotateY, 0);
                Gizmos.matrix = Matrix4x4.TRS(monsterInfo.InitPosition + newDir, stableRotation, DetectSize * 2);
                Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
            }
            else
            {
                stableRotation = Quaternion.Euler(0, StableRotateY, 0);
                Gizmos.matrix = Matrix4x4.TRS(transform.position + newDir, stableRotation, DetectSize * 2);
                Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
            }
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
