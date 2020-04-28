using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMove : MonoBehaviour
{
    private GameObject Ground;
    private void Awake()
    {
        Ground = GameObject.FindGameObjectWithTag("Ground"); 
    }
    public void Triggered()
    {
        Ground.GetComponent<Animation>().Play();
    }
}
