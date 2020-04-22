using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallDamager : MonoBehaviour
{
    private GameObject userObject;
    private float finalDamage;

    public void SetValue(GameObject user, float damage)
    {
        userObject = user;
        finalDamage = damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Fire Ball Hit");
        Destroy(gameObject);
    }
}
