using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerSphere : DeciderBase
{
    public bool ShowInScene;        // 是否在場景中顯示範圍
    public Vector3 DetectCenter;    // 偵測範圍中心
    public float DetectRadius = 5;  // 偵測範圍半徑
    public float DetectPauseTime = 0;  // 偵測後幾秒內不再次偵測

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

        Collider[] playerCollider = Physics.OverlapSphere(transform.position + DetectCenter, DetectRadius, LayerMask.GetMask("Player"));
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

        Gizmos.color = new Color(1, 0, 0, 0.4f);
        Vector3 target = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0) * new Vector3(0, 0, DetectRadius);
        Gizmos.DrawSphere(transform.position + DetectCenter, DetectRadius);
    }
}
