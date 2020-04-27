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
    public float MaxSpeed = 2;
    private bool isLive;
    private bool isSetMousePos;
    private float Speed;
    private float MaxHP;
    private float MaxMP;
    public float CurrentHP; 
    private float MP;
    private float ATK;
    private Vector3 MouseStartPos; 
    private GameObject PositionUI;

    private void Awake()
    {
        PositionUI = GameObject.FindGameObjectWithTag("PositionUI");
        Playerani = GetComponent<Animator>();
        MaxHP = 100;
        MaxMP = 100;
    }
    // Start is called before the first frame update
    void Start()
    {
        CurrentHP = MaxHP;
        MP = MaxMP;
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
                if (Speed >= MaxSpeed)
                {
                    Speed = MaxSpeed;
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
                transform.Rotate(transform.up * 1.5f);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(transform.up * -1.5f);
            }
            if (Input.GetMouseButton(1))
            {
                this.transform.GetChild(2).transform.Rotate(-Input.GetAxis("Mouse Y"),0,0);
            }
            else if (Input.GetMouseButtonUp(1))
            {
                isSetMousePos = false;
            }
            GetRecovery();
        }
    }
    public void GetHurt(float damage) {
        CurrentHP -= damage;
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
    public float GetMaxMP()
    {
        return MaxMP;
    }
    public void GetRecovery()
    {
        CurrentHP += MaxHP * 0.0005f;
        if (CurrentHP > MaxHP) CurrentHP = MaxHP;
        MP += MaxMP * 0.0005f;
        if (MP > MaxMP) MP = MaxMP;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MonsterField")
        {
            PositionUI.GetComponent<PositionMessage>().showMessage(other.gameObject.GetComponent<MonsterField>().GetFieldName());
        }
        else if(other.tag == "Monster")
        {
            other.gameObject.GetComponent<MonsterInfo>().GetDamage(ATK);
        }
    }
}
