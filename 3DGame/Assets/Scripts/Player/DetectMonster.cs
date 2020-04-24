using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectMonster : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Monster")
        {
            Debug.Log(collision.gameObject.tag);
        }
    }
}
