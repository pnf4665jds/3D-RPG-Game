using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapExistGroup :trapGroup
{
    public List<GameObject> existedTraps = new List<GameObject>();

    public override IEnumerator switchON() {

       yield return new WaitForSeconds(DeltaTime);
       foreach (GameObject trap in existedTraps) {
           trap.GetComponent<Animation>().Play();
           StartCoroutine(trap.GetComponent<trapDetail>().shoot()); 

       }
    }
    public override void switchOFF()
    {
        foreach (GameObject trap in existedTraps)
         {
             trap.GetComponent<Animation>().Stop() ;

         }
    }

}
