using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionKumataAttack : ActionBase
{
    // 這個Action用來讓Kumata進行普通砍擊
 
    public float Damage;    // 傷害
    public string TriggerName;  // 要呼叫的Trigger名稱

    [Header("Damage Area")]
    public bool ShowInScene;        // 是否在場景中顯示範圍
    public Vector3 DamageAreaCenter;    // 傷害範圍中心
    public Vector3 DamageAreaSize = new Vector3(1, 1, 1);      // 傷害範圍大小
    public float AreaKeepTime;      // 傷害範圍持續時間

    [Header("Effect")]
    public GameObject Effect;
    public float EffectDelay;


    public override void Init()
    {
        animator.SetTrigger(TriggerName);
        GameObject area = new GameObject();
        DamageAreaCreator creator = area.AddComponent<DamageAreaCreator>();
        creator.CreateCubeArea(transform.position + GetCenterVector(), transform.rotation, DamageAreaSize, Damage, AreaKeepTime);
        StartCoroutine(StartEffect());
    }

    public override void Process()
    {

    }

    public override void Exit()
    {

    }

    public IEnumerator StartEffect()
    {
        yield return new WaitForSeconds(EffectDelay);
        GameObject effect = Instantiate(Effect, transform);
        effect.transform.position = transform.position + new Vector3(-1, 1, 0);
        Destroy(effect, 3);
    }

    /// <summary>
    /// 取得範圍框的中心
    /// </summary>
    /// <returns></returns>
    public Vector3 GetCenterVector()
    {
        return Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * DamageAreaCenter;
    }

    /// <summary>
    /// 在Scene中繪製對應的範圍
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (!ShowInScene)
            return;

        Vector3 newDir = GetCenterVector();
        Gizmos.matrix = Matrix4x4.TRS(transform.position + newDir, transform.rotation, DamageAreaSize * 2);
        Gizmos.color = new Color(1, 0, 0, 0.4f);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        
    }
}
