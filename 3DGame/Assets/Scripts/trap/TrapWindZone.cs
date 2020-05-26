using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapWindZone : MonoBehaviour
{
    public bool ShowInScene;        // 是否在場景中顯示範圍
    public float Rate = 1;
    public float MaxSpeed = 1;

    public Vector3 DetectCenter;    // 偵測範圍中心
    public Vector3 DetectSize = new Vector3(1, 1, 1);      // 偵測範圍大小

    private Collider[] playerCollider;
    private Rigidbody rigid;
    private bool inZone = false;

    private void Update()
    {
        Detect();
        if (inZone)
            ApplyForce();
    }

    /// <summary>
    /// 偵測玩家
    /// </summary>
    private void Detect()
    {
        Vector3 newDir = GetCenterVector();
        playerCollider = Physics.OverlapBox(transform.position + newDir, DetectSize, transform.rotation, LayerMask.GetMask("Player"));

        if (playerCollider.Length <= 0)
        { 
            inZone = false;
            return;
        }

        inZone = true;
        rigid = playerCollider[0].transform.root.GetComponentInChildren<Rigidbody>();
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
    /// 在Scene中繪製對應的範圍
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (!ShowInScene)
            return;

        Vector3 newDir = GetCenterVector();
        Gizmos.matrix = Matrix4x4.TRS(transform.position + newDir, transform.rotation, DetectSize * 2);
        Gizmos.color = new Color(1, 0, 0, 0.4f);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        
    }

    private void ApplyForce()
    {
        if(rigid && rigid.velocity.sqrMagnitude < MaxSpeed * MaxSpeed)
            rigid.AddForce(GetCenterVector().normalized * Rate);
    }
}
