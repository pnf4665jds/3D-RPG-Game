using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    public float Damage;
    public float MaxSpeed;
    public LayerMask DestroyMask;

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
        else if((1 << other.gameObject.layer & DestroyMask) == 1 << other.gameObject.layer)
        {
            // gameObject.layer 會得到 Layer 的排序（0,1,2,3,4,5,6……)
            // LayerMask 會得到得到二進位數值 (0001 1010)
            // & 則是做 bit 運算，且 << 優先於 &
            // 所以等號左側會得到一個二進位數字表示 gameObject的layer是否在Mask中
            Debug.Log(other.gameObject.name);
            Destroy(gameObject);
        }
    }
}
