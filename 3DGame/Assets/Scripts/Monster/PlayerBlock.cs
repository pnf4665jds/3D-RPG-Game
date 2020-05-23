using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerBlock : MonoBehaviour
{
    public Transform FollowTransform;

    private Collider blockCollider;

    // Start is called before the first frame update
    void Start()
    {
        blockCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        blockCollider.gameObject.transform.position = FollowTransform.position;
    }
}
