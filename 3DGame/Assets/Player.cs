using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator Playerani;
    public bool isAttack;
    public bool isHit;
    public bool isMove;
    public bool isDie;
    private float Speed;
    // Start is called before the first frame update
    void Start()
    {
        Playerani = GetComponent<Animator>();
        isAttack = false;
        isHit = false;
        isMove = false;
        isDie = false;
        Speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            if (Speed >= 1)
            {
                Speed = 1;
            }
            else Speed += Time.deltaTime;
        }
        else Speed = 0;
        if (Input.GetMouseButton(0))
        {
            isAttack = true;
            Playerani.SetBool("isAttack",isAttack);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isAttack = false;
            Playerani.SetBool("isAttack",isAttack);
        }
        if (Input.GetKey(KeyCode.W))
        {
            isMove = true;
            transform.Translate(Vector3.forward * Speed*0.1f);
            Playerani.SetBool("isMove",isMove);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            isMove = false;
            Playerani.SetBool("isMove",isMove);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(transform.up * 0.5f);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(transform.up * -0.5f);
        }
    }
}
