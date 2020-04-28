using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDamager : MonoBehaviour
{
    private GameObject userObject;
    private float finalDamage;

    public void SetValue(GameObject user, float damage)
    {
        userObject = user;
        finalDamage = damage;
    }

    private void OnParticleCollision(GameObject other)
    {
        other.GetComponent<Player>().GetHurt(finalDamage);
        //Debug.Log(other.gameObject.GetComponent<Player>().GetCurHP());
    }
}
