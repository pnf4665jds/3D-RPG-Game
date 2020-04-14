using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameAttack : ActionBase
{
    // 火焰特效物件
    public GameObject FlameObject;
    // 龍的頭部
    public GameObject Head;
    // 微調噴火位置用
    public Vector3 Offset;
    // 特效出現的延遲
    public float DelayBeforeEffect;
    // 特效持續時間
    public float KeepTime;

    private bool colDetect = false;
    private GameObject flameObject;

    public override void Init()
    {
        colDetect = true;
        animator.SetTrigger("FlameAttack");
        StartCoroutine(StartEffect());
    }

    public override void Process()
    {

    }

    public override void Exit()
    {
        colDetect = false;
    }

    /// <summary>
    /// 經過Delay後產生火焰特效物件
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartEffect()
    {
        yield return new WaitForSeconds(DelayBeforeEffect);
        flameObject = Instantiate(FlameObject, Head.transform);
        yield return new WaitForSeconds(KeepTime);
        // 先停止產生粒子，並等待剩餘粒子消失後再破壞物體
        flameObject.GetComponent<ParticleSystem>().Stop();
        yield return new WaitForSeconds(5);
        Destroy(flameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (controller.CurrentStateName != UseStateName)
            return;

        if (collision.gameObject.tag == "Player")
            Debug.Log("Hit" + gameObject.name);
    }
}
