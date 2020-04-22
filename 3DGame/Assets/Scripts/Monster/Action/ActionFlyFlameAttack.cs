using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionFlyFlameAttack : ActionBase
{
    public float Damage;
    // 偵測玩家的方框
    public DeciderDetectPlayerRect DetectRect;
    // 龍的頭部
    public GameObject Head;
    // 火球物件
    public GameObject FireBallObject;
    // 一次噴的火球數量
    public int BallNum;
    // 特效出現的延遲
    public float DelayBeforeEffect;
    // 間隔時間
    public float ShootDeltaTime;
    // 火球速度
    public float FireBallSpeed = 10;

    private Vector3 center;
    private Vector3 target;
    private Vector3 range;

    public override void Init()
    {
        center = transform.position + DetectRect.GetCenterVector();
        range = DetectRect.DetectSize / 2;
        StartCoroutine(ShootFireBalls());
    }

    public override void Process()
    {

    }

    public override void Exit()
    {

    }

    private IEnumerator ShootFireBalls()
    {
        animator.SetTrigger("FlyFlameAttack");
        yield return new WaitForSeconds(DelayBeforeEffect);
        for (int i = 0; i < BallNum; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(center, Vector3.down, out hit, Mathf.Infinity, 1 << 9))
            {
                target = hit.point + GetRandomRange();
                GameObject fireBall = Instantiate(FireBallObject, Head.transform.position, Quaternion.identity);
                FireBallDamager damager = fireBall.AddComponent<FireBallDamager>();
                damager.SetValue(gameObject, Damage);
                fireBall.GetComponent<Rigidbody>().AddForce((target - fireBall.transform.position) * FireBallSpeed);
                Destroy(fireBall, 10);
                yield return new WaitForSeconds(ShootDeltaTime);
            }
        }
    }

    private Vector3 GetRandomRange()
    {
        Vector3 v = new Vector3(range.x * Random.Range(-1f, 1f), 0, range.z * Random.Range(-1f, 1f));
        return Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * v;
    }
}
