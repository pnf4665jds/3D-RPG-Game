using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAreaCreator : MonoBehaviour
{
    // 產生傷害區域

    private float damage;

    // 產生立方體區域
    public void CreateCubeArea(Vector3 pos, Quaternion rot, Vector3 size, float Damage, float keepTime)
    {
        transform.position = pos;
        transform.rotation = rot;
        gameObject.tag = "DamageArea";
        damage = Damage;
        BoxCollider collider = gameObject.AddComponent<BoxCollider>();
        collider.isTrigger = true;
        collider.size = size * 2;
        Destroy(gameObject, keepTime);
    }

    // 產生球體區域
    public void CreateSphereArea(Vector3 pos, float radius, float Damage, float keepTime)
    {
        transform.position = pos;
        gameObject.tag = "DamageArea";
        damage = Damage;
        SphereCollider collider = gameObject.AddComponent<SphereCollider>();
        collider.isTrigger = true;
        collider.radius = radius;
        Destroy(gameObject, keepTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().GetHurt(damage);
            Debug.Log("Area Hit!");
        }
    }
}
