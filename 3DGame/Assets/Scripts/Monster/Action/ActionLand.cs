using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionLand : ActionBase
{
    // 這個Action用來讓怪物進行著地動作

    public float LandSpeed = 1;

    public override void Init()
    {
        animator.SetTrigger("Land");
    }

    public override void Process()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, 1 << 9))
        {
            if(Vector3.Distance(hit.point, transform.position) < 0.1f)
            {
                // 開啟重力
                GetComponent<Rigidbody>().useGravity = true;
            }
            else
            {
                Land();
            }
        }
    }

    public override void Exit()
    {
        
    }

    /// <summary>
    /// 降落
    /// </summary>
    private void Land()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.down, monsterInfo.MoveSpeed * Time.deltaTime * LandSpeed);
    }
}
