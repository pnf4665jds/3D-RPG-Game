using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    public float Damage;
    public float MaxSpeed;

    protected GameObject shooter;  // 發射者
    protected GameObject player;

    private void Start()
    {
        Init();
    }

    public void SetValue(GameObject shooter, float damage)
    {
        this.shooter = shooter;
        Damage = damage;
    }

    protected virtual void Init()
    {
        player = GameObject.FindWithTag("Player");
        StartCoroutine(Shoot());
    }

    // 需實作射擊內容
    protected abstract IEnumerator Shoot();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().GetHurt(Damage);

            Destroy(gameObject);
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            Destroy(gameObject);
        }
    }
}
