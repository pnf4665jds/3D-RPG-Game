using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerRect : DeciderBase
{
    public bool ShowInScene;        // 是否在場景中顯示範圍
    public Vector3 DetectCenter;    // 偵測範圍中心
    public Vector3 DetectSize = new Vector3(1, 1, 1);      // 偵測範圍大小
    //public Quaternion a = new Quaternion(1, 1, 1, 1);

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

        Collider[] playerCollider = Physics.OverlapBox(transform.position + DetectCenter, DetectSize / 2, transform.rotation, LayerMask.GetMask("Player"));
        if (playerCollider.Length > 0)
        {
            controller.CurrentTarget = playerCollider[0].gameObject;
            return true;
        }
        controller.CurrentTarget = null;
        return false;
    }

    /// <summary>
    /// 在Scene中繪製對應的範圍
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (!ShowInScene)
            return;

        Matrix4x4 transMatrix = Matrix4x4.TRS(Vector3.zero, transform.rotation, Vector3.one);
        Matrix4x4 oldMatrix = Gizmos.matrix;

        //Gizmos.matrix *= transMatrix;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = new Color(0, 1, 0, 0.4f);
        Gizmos.DrawWireCube(DetectCenter, DetectSize / 2);
        //Gizmos.matrix = oldMatrix;
    }
}
