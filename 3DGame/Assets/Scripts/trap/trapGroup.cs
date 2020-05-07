using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapGroup : MonoBehaviour
{
    public List<GameObject> traps = new List<GameObject>();
    public float DeltaTime;

    public float getDeltaTime() {
        return DeltaTime;
    }
    public IEnumerator switchON() {

        yield return new WaitForSeconds(DeltaTime);
        foreach (GameObject trap in traps) {
            trap.GetComponent<Animation>().Play();
            
        }
    }
    public void switchOFF() {
        foreach (GameObject trap in traps)
        {
            trap.GetComponent<Animation>().Stop() ;

        }
    }
    
}
