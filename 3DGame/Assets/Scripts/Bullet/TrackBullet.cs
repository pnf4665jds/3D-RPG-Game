using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackBullet : BulletBase
{
    public GameObject Effect;

    private float speed;
    private float timer = 0;

    private void Update()
    {
        if (speed < MaxSpeed)
            speed += MaxSpeed * 0.005f;
    }

    protected override void Init()
    {
        speed = MaxSpeed * 0.5f;
        base.Init();
    }

    protected override IEnumerator Shoot()
    {
        while (timer <= 0.5f)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + shooter.transform.forward, Time.deltaTime * speed);
            yield return null;
        }
        while (true)
        {
            Vector3 target = player.transform.position;
            target.y = transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
            yield return null;
        }
    }

    private void OnDestroy()
    {
        GameObject effect = Instantiate(Effect, transform.position, Quaternion.identity);
        Destroy(effect, 5);
        StopAllCoroutines();
    }
}
