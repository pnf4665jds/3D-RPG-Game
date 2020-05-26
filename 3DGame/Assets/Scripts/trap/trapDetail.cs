using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapDetail : MonoBehaviour
{
    public int hurtValue ;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Player>().GetHurt(hurtValue);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().GetHurt(hurtValue);
        }
    }

}

