using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class trapGroup : MonoBehaviour
{
   

    public float DeltaTime;

    public float getDeltaTime() {
        return DeltaTime;
    }
    public abstract IEnumerator switchON();
    public abstract IEnumerator process();

    public abstract void switchOFF();
    public abstract IEnumerator destroytraps();
    
}

