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

    private GameObject player;

    public override void Init()
    {
        player = GameObject.FindWithTag("Player");
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
            yield return LookAtPlayer();
            animator.SetTrigger(TriggerName);
            yield return new WaitForSeconds(ShootDelayTime);
            GameObject bullet = Instantiate(Bullet, ShootPosition.transform.position, Quaternion.identity);
            Destroy(bullet, BulletKeepTime);
            yield return new WaitForSeconds(ShootDeltaTime);
        }
    }

    private IEnumerator LookAtPlayer()
    {
        animator.SetBool("Walk", true);
        Vector3 playerPos = player.transform.position;
        while (true)
        {
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, playerPos - transform.position, monsterInfo.RotateSpeed * Time.deltaTime, 0.0f);
            newDirection.y = 0;
            if (transform.rotation == Quaternion.LookRotation(newDirection))
                break;
            else
                transform.rotation = Quaternion.LookRotation(newDirection);

            yield return null;
        }
        animator.SetBool("Walk", false);
    }
}
