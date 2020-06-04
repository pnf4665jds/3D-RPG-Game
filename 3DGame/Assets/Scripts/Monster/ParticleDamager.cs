using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDamager : MonoBehaviour
{
    private GameObject userObject;
    private float finalDamage;
    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void SetValue(GameObject user, float damage)
    {
        userObject = user;
        finalDamage = damage;
    }

    // 粒子系統是Collider
    private void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Player")
            other.GetComponent<Player>().GetHurt(finalDamage);
        //Debug.Log(other.gameObject.GetComponent<Player>().GetCurHP());
    }
}
