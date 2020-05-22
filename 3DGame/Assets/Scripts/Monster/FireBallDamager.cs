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
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().GetHurt(finalDamage);
            //Debug.Log(collision.gameObject.GetComponent<Player>().GetCurHP());
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
            Destroy(gameObject);
    }
}
