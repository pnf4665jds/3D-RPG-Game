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
    private bool isLive;
    private bool isSetMousePos;
    private float Speed;
    private float MaxHP;
    private float CurrentHP;
    private float Mana;
    private GameObject PlayerBloodUi;
    private GameObject MPUI;
    private Vector3 MouseStartPos;

    private void Awake()
    {
        PlayerBloodUi = GameObject.FindGameObjectWithTag("PlayerBloodUI");
        MPUI = GameObject.FindGameObjectWithTag("MPUI");
        Playerani = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        CurrentHP = MaxHP;
        isLive = true;
        isSetMousePos = false;
        isAttack = false;
        isHit = false;
        isMove = false;
        isDie = false;
        Speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLive)
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
                Playerani.SetBool("isAttack", isAttack);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ResetAnimation();
            }
            if (Input.GetKey(KeyCode.W))
            {
                isMove = true;
                transform.Translate(Vector3.forward * Speed * 0.1f);
                Playerani.SetBool("isMove", isMove);
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                ResetAnimation();
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(transform.up * 0.5f);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(transform.up * -0.5f);
            }
            if (Input.GetMouseButton(1))
            {
                if (!isSetMousePos)
                {
                    MouseStartPos = Input.mousePosition;
                    isSetMousePos = true;
                }
                Vector3 dis = Input.mousePosition - MouseStartPos;
                if(dis.x >20 || dis.x < -20)
                {
                    this.transform.GetChild(2).transform.Rotate(new Vector3(0,dis.x*0.01f,0));
                }
                else if(dis.y >20 || dis.y < -20)
                {
                    this.transform.GetChild(2).transform.Rotate(new Vector3(dis.y*0.01f,0,0));
                }
            }
            else if (Input.GetMouseButtonUp(1))
            {
                isSetMousePos = false;
            }
        }
    }
    public void GetDamage(float damage) {
        CurrentHP -= damage;
        PlayerBloodUi.transform.GetChild(0).transform.GetChild(0).GetComponent<PlayerBlood>().GetHurt(damage);
        if(CurrentHP <=0)
        {
            isLive = false;
            isDie = true;
            Playerani.SetBool("isDie", isDie);
        }
    }
    public void ResetAnimation()
    {
        isAttack = false;
        isHit = false;
        isMove = false;
        Playerani.SetBool("isAttack",isAttack);
        Playerani.SetBool("isHit",isHit);
        Playerani.SetBool("isMove", isMove);
    }
}
