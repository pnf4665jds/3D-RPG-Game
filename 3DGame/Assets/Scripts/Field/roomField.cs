﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomField : MonoBehaviour
{

    public List<trapGroup> tgs = new List<trapGroup>();
    

    // Start is called before the first frame update
    public void Start()
    {
        /*foreach (trapGroup tg in tgs)
        {
            StartCoroutine(tg.switchON());
        }*/
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player") {
            Debug.Log("Enter");
            
            foreach (trapGroup tg in tgs)
            {
                StartCoroutine(tg.switchON());
            }
            
        }
    }
    private void OnTriggerStay(Collider other) {

        if (other.tag == "Player")
        {

            

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            
            foreach (trapGroup tg in tgs)
            {
                tg.switchOFF();
            }
        }
    }

}
