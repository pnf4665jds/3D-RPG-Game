using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackBullet : MonoBehaviour
{
    public float Damage;
    public GameObject Effect;
    public float Speed;

    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * Speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().GetHurt(Damage);
            GameObject effect = Instantiate(Effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(effect, 5);
        }
    }
}
