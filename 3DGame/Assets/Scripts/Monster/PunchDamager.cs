using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchDamager : MonoBehaviour
{
    private GameObject userObject;
    private float finalDamage;
    // Start is called before the first frame update
    public void SetValue(GameObject user, float damage)
    {
        userObject = user;
        finalDamage = damage;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().GetHurt(finalDamage);
            //Debug.Log(other.gameObject.GetComponent<Player>().GetCurHP());
        }
    }
}
