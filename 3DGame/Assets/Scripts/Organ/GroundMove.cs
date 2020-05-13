using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMove : MonoBehaviour
{
    private GameObject Ground;
    private bool isTrigger;
    private void Awake()
    {
        Ground = GameObject.FindGameObjectWithTag("Ground");
        isTrigger = false;
    }
    public void Triggered()
    {
        if (!isTrigger) {
            Ground.GetComponent<Animation>().Play();
            isTrigger = true;
        }
        
    }
}
