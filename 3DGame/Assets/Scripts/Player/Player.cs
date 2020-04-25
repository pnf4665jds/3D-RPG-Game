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
    private float MP;
    private float ATK;
    private GameObject PlayerBloodUi;
    private GameObject MPUI;
    private Vector3 MouseStartPos;

    private void Awake()
    {
        PlayerBloodUi = GameObject.FindGameObjectWithTag("PlayerBloodUI");
        MPUI = GameObject.FindGameObjectWithTag("MPUI");
        Playerani = GetComponent<Animator>();
        MaxHP = 100;
        MP = 100;
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
                transform.Rotate(transform.up * 2f);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(transform.up * -2f);
            }
            if (Input.GetMouseButton(1))
            {
                this.transform.GetChild(2).transform.Rotate(-Input.GetAxis("Mouse Y"),0,0);
            }
            else if (Input.GetMouseButtonUp(1))
            {
                isSetMousePos = false;
            }
        }
    }
    public void GetDamage(float damage) {
        CurrentHP -= damage;
        //PlayerBloodUi.transform.GetChild(0).transform.GetChild(0).GetComponent<PlayerBlood>().GetHurt(damage);
        if(CurrentHP <=0)
        {
            isLive = false;
            isDie = true;
            Playerani.SetBool("isDie", isDie);
        }
    }
    public void Healing(float healing)
    {
        CurrentHP += healing;
        if (CurrentHP > MaxHP) CurrentHP = MaxHP;
        //UI update HP
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
    public float GetMaxHP()
    {
        return MaxHP;
    }
    public float GetCurHP()
    {
        return CurrentHP;
    }
    public float GetMP()
    {
        return MP;
    }
}
