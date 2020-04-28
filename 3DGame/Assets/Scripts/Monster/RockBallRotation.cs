using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBallRotation : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Golem;
    public void rotate() {
        this.transform.GetComponentInParent<Transform>().rotation = Golem.transform.rotation;
    }
}
