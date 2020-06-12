using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    private Animator animator;
    private bool isBroken;
    public int Money;
    public int HealthItemNum;
    public int ManaItemNum;
    public int ReliveItemnum;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        isBroken = false;
    }
    private void Start()
    {
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Player>().GetisAttack() && !isBroken)
            {
                isBroken = true;
                animator.SetBool("isBroken", isBroken);
                GetItem();
                Destroy(this.GetComponent<BoxCollider>());
                Destroy(this.gameObject,5);
            }
        }
    }
    private void GetItem()
    {
        DropItemSystem.instance.createMoney(transform.position,Money);
        DropItemSystem.instance.createHealthItem(transform.position, HealthItemNum);
        DropItemSystem.instance.createManaItem(transform.position,ManaItemNum);
        //DropItemSystem.instance.createRecoveryItem(transform.position,ReliveItemnum);
    }
}
