using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapCloneGroup :trapGroup
{
    public GameObject trapPos;
    public GameObject trapPref;
    private GameObject trapClone;
    public bool loop ;
    public bool isShoot;
    private Vector3 createPos;
    public float shootDistance;
    public float rotation;
    public int Speed;
    private IEnumerator pro;

    public override IEnumerator switchON()
    {

        yield return new WaitForSeconds(DeltaTime);

        createPos = trapPos.transform.position;

        yield return new WaitUntil(() => createClone());

        trapClone.GetComponent<Animation>().Play();
        pro = process();
        StartCoroutine(pro);

        

    }
    public override IEnumerator process() {

        while (loop) {
            
            while (Vector3.Distance(trapClone.transform.position, createPos) < shootDistance && isShoot)
            {
                
                trapClone.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                yield return null;
            }
            trapClone.transform.position = createPos;
            
        }
        
    }
    public override void switchOFF()
    {
        Destroy(trapClone.gameObject);
        StopCoroutine(pro);
        loop = false;
    }

    public override IEnumerator destroytraps()
    {
        yield return null;
    }
    public bool createClone() {
        trapClone = Instantiate(trapPref, createPos, Quaternion.Euler(0 , rotation , 0));

        return true;
    }



}
