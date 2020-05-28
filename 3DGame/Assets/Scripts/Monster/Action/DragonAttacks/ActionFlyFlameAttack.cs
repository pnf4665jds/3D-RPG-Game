using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionFlyFlameAttack : ActionBase
{
    // 這個Action用來讓火龍在飛行狀態下噴火球

    public float Damage;                // 攻擊傷害
    public GameObject Head;             // 龍的頭部
    public GameObject FireBallObject;   // 火球物件
    public int BallNum;                 // 一次噴的火球數量
    public float DelayBeforeEffect;     // 特效出現的延遲
    public float ShootDeltaTime;        // 間隔時間
    public float FireBallSpeed = 10;    // 火球速度

    private Vector3 target;

    public override void Init()
    {
        StartCoroutine(ShootFireBalls());
    }

    public override void Process()
    {

    }

    public override void Exit()
    {

    }

    // 噴火球攻擊
    private IEnumerator ShootFireBalls()
    {
        animator.SetTrigger("FlyFlameAttack");
        yield return new WaitForSeconds(DelayBeforeEffect);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < BallNum; i++)
        {
            target = player.transform.position;
            GameObject fireBall = Instantiate(FireBallObject, Head.transform.position, Quaternion.identity);
            FireBallDamager damager = fireBall.AddComponent<FireBallDamager>();
            damager.SetValue(gameObject, Damage);
            fireBall.GetComponent<Rigidbody>().AddForce((target - fireBall.transform.position) * FireBallSpeed);
            Destroy(fireBall, 10);
            yield return new WaitForSeconds(ShootDeltaTime);    
        }
    }
}
