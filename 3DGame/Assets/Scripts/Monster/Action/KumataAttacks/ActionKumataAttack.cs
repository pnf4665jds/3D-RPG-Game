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
    public float AreaDelayTime;     // 出現傷害範圍的延遲時間
    public float AreaKeepTime;      // 傷害範圍持續時間

    [Header("Effect")]
    public GameObject Effect;
    public float EffectDelay;

    public override void Init()
    {
        animator.SetTrigger(TriggerName);
        StartCoroutine(SetDamageArea());
        StartCoroutine(StartEffect());
    }

    public override void Process()
    {

    }

    public override void Exit()
    {

    }

    public IEnumerator SetDamageArea()
    {
        yield return new WaitForSeconds(AreaDelayTime);
        DamageAreaCreator.instance.CreateCubeArea(transform.position + monsterInfo.GetCenterVector(DamageAreaCenter), transform.rotation, DamageAreaSize, Damage, AreaKeepTime);
    }

    public IEnumerator StartEffect()
    {
        yield return new WaitForSeconds(EffectDelay);
        GameObject effect = Instantiate(Effect, transform);
        effect.transform.position = transform.position + new Vector3(-1, 1, 0);
        Destroy(effect, 3);
        SoundSystem.instance.PlaySound(Source, ActionSound, Volume, SoundDelay, false);
        SoundSystem.instance.PlaySound(Source, ActionSound, Volume, SoundDelay, false);
    }

    /// <summary>
    /// 在Scene中繪製對應的範圍
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (!ShowInScene)
            return;

        Vector3 newDir = monsterInfo ? monsterInfo.GetCenterVector(DamageAreaCenter) : Vector3.zero;
        Gizmos.matrix = Matrix4x4.TRS(transform.position + newDir, transform.rotation, DamageAreaSize * 2);
        Gizmos.color = new Color(1, 0, 0, 0.4f);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        
    }
}
