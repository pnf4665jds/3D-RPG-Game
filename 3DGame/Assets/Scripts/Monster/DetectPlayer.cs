using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public bool ShowInScene;
    public float DetectLength = 5;
    public float StopLength = 10;    // 偵測到玩家後，玩家與怪物距離多遠時停止偵測
    public GameObject PlayerObj { get; set; }

    private void Update()
    {
        Detect();
    }

    /// <summary>
    /// 偵測玩家
    /// </summary>
    private void Detect()
    {
        Collider[] playerCollider = Physics.OverlapSphere(transform.position, DetectLength, LayerMask.GetMask("Player"));
        if (playerCollider.Length > 0)
        {
            PlayerObj = playerCollider[0].gameObject;
        }
        else if(PlayerObj != null && Vector3.Distance(transform.position, PlayerObj.transform.position) > StopLength)
        {
            PlayerObj = null;
        }
    }

    /// <summary>
    /// 在Scene中繪製對應的範圍
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (!ShowInScene)
            return;

        Gizmos.color = new Color(0, 1, 0, 0.4f);
        Vector3 target = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0) * new Vector3(0, 0, DetectLength);
        Gizmos.DrawSphere(transform.position, DetectLength);
    }
}
