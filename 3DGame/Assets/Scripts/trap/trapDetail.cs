using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapDetail : MonoBehaviour
{
    private bool stopShoot = false;
    public bool isShoot;
    private float liveTime;
    private Vector3 createPos;
    public float shootDistance;
    // Start is called before the first frame update
    private void Start()
    {
        createPos = this.transform.position;
    }
    private void Update()
    {
        if (Vector3.Distance(this.transform.position, createPos) > shootDistance)
        {
            stopShoot = true;
            Destroy(this.gameObject, 3);
        }

    }
    public IEnumerator shoot()
    {

        while (!stopShoot && isShoot)
        {

            this.transform.Translate(Vector3.forward * 20 * Time.deltaTime);

            yield return null;
        }


    }
    public bool isShooting() {
        return !stopShoot;
    }
}

