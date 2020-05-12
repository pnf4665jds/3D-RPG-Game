using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionShoot : ActionBase
{
    public string TriggerName;
    public GameObject Bullet;   // 發射的子彈物件
    public GameObject ShootPosition;    // 發射口位置
    public float BulletKeepTime;    // 子彈持續時間
    public float ShootDelayTime;    // 射擊延遲時間
    public int BulletNum;           // 射擊數量
    public float ShootDeltaTime;    // 發射間隔時間

    public override void Init()
    {
        StartCoroutine(Shoot());
        animator.SetBool("ReadyShoot", true);
    }

    public override void Process()
    {

    }

    public override void Exit()
    {
        animator.SetBool("ReadyShoot", false);
    }

    private IEnumerator Shoot()
    {
        for(int i = 0; i < BulletNum; i++)
        {
            animator.SetTrigger(TriggerName);
            yield return new WaitForSeconds(ShootDelayTime);
            GameObject bullet = Instantiate(Bullet, ShootPosition.transform.position, Quaternion.identity);
            Destroy(bullet, BulletKeepTime);
            yield return new WaitForSeconds(ShootDeltaTime);
        }
    }
}
