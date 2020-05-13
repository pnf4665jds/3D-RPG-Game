using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackBullet : MonoBehaviour
{
    public float Damage;
    public GameObject Effect;
    public float MaxSpeed;

    private GameObject shooter;  // 發射者
    private GameObject player;
    private float speed;
    private float timer = 0;

    private void Start()
    {
        shooter = FindObjectOfType<ActionShoot>().gameObject;
        player = GameObject.FindWithTag("Player");
        speed = MaxSpeed * 0.5f;
        StartCoroutine(Shoot());
    }

    private void Update()
    {
        if (speed < MaxSpeed)
            speed += MaxSpeed * 0.005f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().GetHurt(Damage);
            
            Destroy(gameObject);
        }
    }

    private IEnumerator Shoot()
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
