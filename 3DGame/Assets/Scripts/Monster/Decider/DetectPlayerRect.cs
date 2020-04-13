using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerRect : DeciderBase
{
    public bool ShowInScene;        // 是否在場景中顯示範圍
    public Vector3 DetectCenter;    // 偵測範圍中心
    public Vector3 DetectSize = new Vector3(1, 1, 1);      // 偵測範圍大小
    public float DetectPauseTime = 0;  // 偵測後幾秒內不再次偵測
    //public Quaternion a = new Quaternion(1, 1, 1, 1);

    private bool PauseTimeFinish = true;

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

        if (!PauseTimeFinish & DetectPauseTime > 0)
            return false;

        Vector3 newDir = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * DetectCenter;
        Collider[] playerCollider = Physics.OverlapBox(transform.position + newDir, DetectSize / 2, transform.rotation, LayerMask.GetMask("Player"));
        if (playerCollider.Length > 0)
        {
            if (DetectPauseTime > 0)
                StartCoroutine(PauseTimer());

            controller.CurrentTarget = playerCollider[0].gameObject;
            return true;
        }
        return false;
    }

    /// <summary>
    /// 偵測暫停計時器
    /// </summary>
    /// <returns></returns>
    private IEnumerator PauseTimer()
    {
        PauseTimeFinish = false;
        yield return new WaitForSeconds(DetectPauseTime);
        PauseTimeFinish = true;
    }

    /// <summary>
    /// 在Scene中繪製對應的範圍
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (!ShowInScene)
            return;

        Vector3 newDir = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * DetectCenter;
        Gizmos.matrix = Matrix4x4.TRS(transform.position + newDir, transform.rotation, DetectSize);
        //Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = new Color(1, 0, 0, 0.4f);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);

        //Gizmos.matrix = Matrix4x4.TRS(transform.position + DetectCenter, transform.rotation, DetectSize);
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        // to visualize this:
    }
}
