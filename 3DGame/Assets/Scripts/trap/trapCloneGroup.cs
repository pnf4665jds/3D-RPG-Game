using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapCloneGroup :trapGroup
{
    public GameObject trapPos;
    public GameObject trapPref;
    private GameObject trapClone;
    public bool loop ;
    private bool stopShoot = false;
    public bool isShoot;
    private Vector3 createPos;
    public float shootDistance;
    // Start is called before the first frame update
    private void Start()
    {
        createPos = trapPos.transform.position;
    }
    private void Update()
    {
        if (Vector3.Distance(trapPos.transform.position, createPos) > shootDistance)
        {
            stopShoot = true;
        }

    }

    public override IEnumerator switchON() {

        while (loop) {
            
            yield return new WaitForSeconds(DeltaTime);
            
            trapPos.transform.position = createPos;

            yield return new WaitUntil(() =>CloneTrap());
            
            stopShoot = false;

            yield return new WaitUntil(() => stopShoot);
            Destroy(trapClone.gameObject);

        }
        
    }
    public override IEnumerator process() {
        //yield return new W
        while (!stopShoot && isShoot)
        {
            trapClone.GetComponent<Animation>().Play();
            trapClone.transform.Translate(Vector3.forward * 20 * Time.deltaTime);
            trapPos.transform.Translate(Vector3.forward * 20 * Time.deltaTime);
            yield return null;
        }
    }
    public override void switchOFF()
    {
        loop = false;
    }
    public override IEnumerator destroytraps()
    {
        yield return null;
    }
    private bool CloneTrap()
    {
        trapClone = Instantiate(trapPref, trapPos.transform.position, Quaternion.identity);
        return true;
    }


}
