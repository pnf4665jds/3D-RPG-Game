using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionFlyFlameAttack : ActionBase
{
    // 這個Action用來讓火龍在飛行狀態下進行噴火攻擊

    [Header("Flame")]
    public float Damage;            // 傷害
    public GameObject FlameObject;  // 火焰特效物件
    public GameObject Head;         // 龍的頭部
    public Vector3 Offset;          // 微調噴火位置用位移
    public float DelayBeforeEffect; // 特效出現的延遲
    public float KeepTime;          // 特效持續時間
    public float AttackDeltaY = 10;

    private GameObject flameObject;
    private GameObject player;

    public override void Init()
    {
        player = GameObject.FindWithTag("Player");
        StartCoroutine(Move(true));
        StartCoroutine(StartEffect());
    }

    public override void Process()
    {
        LookAtPlayer();
    }

    public override void Exit()
    {

    }

    /// <summary>
    /// 經過Delay後產生火焰特效物件
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartEffect()
    {
        yield return new WaitForSeconds(3);
        animator.SetTrigger("FlyFlameAttack");
        SoundSystem.instance.PlaySound(Source, ActionSound, Volume, SoundDelay, false);
        yield return new WaitForSeconds(DelayBeforeEffect);
        flameObject = Instantiate(FlameObject, Head.transform);
        ParticleDamager damager = flameObject.AddComponent<ParticleDamager>();
        damager.SetValue(gameObject, Damage);
        yield return new WaitForSeconds(KeepTime);
        // 先停止產生粒子，並等待剩餘粒子消失後再破壞物體
        flameObject.GetComponent<ParticleSystem>().Stop();
        StartCoroutine(Move(false));
        yield return new WaitForSeconds(5);
        Destroy(flameObject);
    }

    /// <summary>
    /// 瞄準玩家
    /// </summary>
    private void LookAtPlayer()
    {
        Vector3 finalPos = player.transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, finalPos - transform.position, monsterInfo.RotateSpeed * Time.deltaTime, 0.0f);
        if(flameObject != null)
            flameObject.transform.rotation = Quaternion.LookRotation(finalPos - flameObject.transform.position);
        newDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private IEnumerator Move(bool down)
    {
        Vector3 finalPos = transform.position;
        finalPos.y = down ? transform.position.y - AttackDeltaY : transform.position.y + AttackDeltaY;

        while (Mathf.Abs(transform.position.y - finalPos.y) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, finalPos, monsterInfo.MoveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
