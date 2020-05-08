using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapCloneGroup :trapGroup
{
    public List<GameObject> trapsPos = new List<GameObject>();
    public GameObject trapPref;

    public override IEnumerator switchON() {
        yield return new WaitForSeconds(DeltaTime);
        foreach (GameObject pos in trapsPos)
        {
            GameObject trap = Instantiate(trapPref, pos.transform.position, Quaternion.identity);
            trap.GetComponent<Animation>().Play();
            StartCoroutine(trap.GetComponent<trapDetail>().shoot());

        }
    }
    public override void switchOFF()
    {
        
    }
}
