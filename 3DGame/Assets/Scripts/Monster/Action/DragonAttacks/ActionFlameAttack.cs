using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionFlameAttack : ActionBase
{
    // 這個Action用來讓火龍進行噴火攻擊

    [Header("Flame")]
    public float Damage;            // 傷害
    public GameObject FlameObject;  // 火焰特效物件
    public GameObject FlameObjectReal;
    public GameObject Head;         // 龍的頭部
    public Vector3 Offset;          // 微調噴火位置用位移
    public float DelayBeforeEffect; // 特效出現的延遲
    public float KeepTime;          // 特效持續時間

    private GameObject flameObject;
    private GameObject flameObjectReal;

    public override void Init()
    {
        animator.SetTrigger("FlameAttack");
        StartCoroutine(StartEffect());
    }

    public override void Process()
    {

    }

    public override void Exit()
    {
        StopAllCoroutines();
        if(flameObject)
            Destroy(flameObject);
        if(flameObjectReal)
            Destroy(flameObjectReal);
        Source.enabled = false;
    }

    /// <summary>
    /// 經過Delay後產生火焰特效物件
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartEffect()
    {
        SoundSystem.instance.PlaySound(Source, ActionSound, Volume, SoundDelay, false);
        yield return new WaitForSeconds(DelayBeforeEffect);
        flameObject = Instantiate(FlameObject, Head.transform);
        flameObjectReal = Instantiate(FlameObjectReal, Head.transform);
        flameObject.transform.localRotation = Quaternion.Euler(new Vector3(20, 0, 0));
        flameObjectReal.transform.localRotation = Quaternion.Euler(new Vector3(20, 0, 0));
        ParticleDamager damager = flameObjectReal.AddComponent<ParticleDamager>();
        damager.SetValue(gameObject, Damage);
        yield return new WaitForSeconds(KeepTime);
        // 先停止產生粒子，並等待剩餘粒子消失後再破壞物體
        flameObject.GetComponent<ParticleSystem>().Stop();
        flameObjectReal.GetComponent<ParticleSystem>().Stop();
        yield return new WaitForSeconds(5);
        Destroy(flameObject);
        Destroy(flameObjectReal);
    }
}
