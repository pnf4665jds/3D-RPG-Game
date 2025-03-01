﻿using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator Playerani;
    public bool isAttack;
    public bool isMove;
    public bool isDie;
    public bool UseSkill;
    private bool isLive;
    private bool isChangeState;
    private bool isTurn = false;
    private bool isInBoss = false;
    [SerializeField]private bool SkillAvail;

    public float MaxSpeed = 12;
    [SerializeField] private float Speed;
    private float MaxHP;
    private float MaxMP;
    private float CurrentHP;
    private float MP;
    private float ATK;
    private float FrameCount;
    private NPC npc;
    private float Cooldown;
    [SerializeField]private GameObject temp;
    [SerializeField]private float CurCooldown;
    [SerializeField]private int Money;

    private Vector3 MouseStartPos;
    private GameObject PositionUI;
    [SerializeField]private ParticleSystem SkillParticle;
    public AnimationClip SkillAnim;
    public TextAsset PlayerDetail;

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
        isAttack = false;
        isMove = false;
        isDie = false;
        UseSkill = false;
        Speed = 0;
        ATK = 75;
        isChangeState = false;
        SkillAvail = true;
        FrameCount = 0;
        CurCooldown = 0;
        SkillParticle = GetComponentInChildren<ParticleSystem>();
        SkillParticle.Stop();
        SetCooldown();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLive)
        {
            if(!GameSystem.instance.isPlayerNormal())
            {
                ResetAnimation();
                ResetSpeed();
            }
            isChangeState = false;
            isTalking(GameSystem.instance.isPlayerTalking());
            isShopping(GameSystem.instance.isPlayerShopping());
            isOpenBP(GameSystem.instance.isPlayerOpenBackPack());
            isNormal(GameSystem.instance.isPlayerNormal());
            isAnimation(GameSystem.instance.isAnimation());
        }
        CheckCooldown();
    }
    public void GetHurt(float damage) {
        if (GameSystem.instance.isPlayerNormal()) {
            CurrentHP -= damage;
            if (CurrentHP <= 0)
            {
                ResetAnimation();
                isLive = false;
                isDie = true;
                Playerani.SetBool("isDie", isDie);
                GameSystem.instance.changeModeDead();
            }
        }
        
    }
    public void Healing(float healing)
    {
        CurrentHP = ((CurrentHP+healing) >= MaxHP) ? MaxHP : (CurrentHP + healing);
    }
    public void ResetAnimation()
    {
        isAttack = false;
        isMove = false;
        Playerani.SetBool("isAttack", isAttack);
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
        if (FrameCount >= 100)
        {
            Healing(MaxHP * 0.01f);
            MPChange(MaxMP*0.03f);
            FrameCount -= 100;
        }
    }
    public bool GetisAttack()
    {
        return isAttack;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MonsterField")
        {
            PositionUI.GetComponent<PositionMessage>().showMessage(other.gameObject.GetComponent<MonsterField>().GetFieldName());
            if (other.gameObject.GetComponent<MonsterField>().IsBossField)
            {
                isInBoss = true;
            }
        }
        else if (other.tag == "Monster" || other.tag == "Boss")
        {
            if (isAttack || UseSkill) other.gameObject.GetComponentInParent<MonsterInfo>().GetDamage(ATK);
        }
        else if (other.tag == "Organ")
        {
            other.gameObject.GetComponent<GroundMove>().Triggered();
        }
        else if (other.tag == "goldCoin" || other.tag == "silverCoin" || other.tag == "copperCoin" || other.tag == "HealthPotion" || other.tag == "ManaPotion" || other.tag == "RecoveryPotion")
        {
            switch (other.tag)
            {
                case "goldCoin":
                    MoneyChange(1000);
                    break;
                case "silverCoin":
                    MoneyChange(500);
                    break;
                case "copperCoin":
                    MoneyChange(100);
                    break;
                case "HealthPotion":
                case "ManaPotion":
                case "RecoveryPotion":
                    DropItemSystem.instance.AddPotionToPack(other.gameObject);
                    break;
            }
            Destroy(other.gameObject);
        }
        else if (other.tag == "director")
        {
            temp = other.gameObject;
            //temp.GetComponent<TimeLineManager>().TimeLinePlay();
            GameSystem.instance.changeModeAnimation();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "MonsterField")
        {
            if (other.gameObject.GetComponent<MonsterField>().IsBossField)
            {
                isInBoss = false;
            }
        }
    }
    public void SetSpeed(float speed) { Speed = speed; }
    public float GetSpeed() { return Speed; }
    public void SpeedChange(bool isMove)
    {
        if (isMove) Speed = (Speed > MaxSpeed) ? MaxSpeed : (Speed += Time.deltaTime * MaxSpeed);
        else Speed = 0;
    }
    
    public void MouseEvent(bool isMove)
    {
        if (Input.GetMouseButton(0) && !isMove) SetAttackanim(true);
        else if (Input.GetMouseButton(1)) this.transform.GetChild(2).transform.Rotate(-Input.GetAxis("Mouse Y"), 0, 0);
        if (Input.GetMouseButtonUp(0)) ResetAnimation();
    }
    public void SetAttackanim(bool isAttack)
    {
        this.isAttack = isAttack;
        Playerani.SetBool("isAttack", isAttack);
    }
    public void ATKChange(float num) {ATK += num;}
    public void MPChange(float num)
    {
        MP = (MP+num > MaxMP) ? MaxMP : (MP + num);
    }
    public void KeyboardEvent(bool isAttack)
    {
        if (!isAttack)
        {
            if (Input.GetKeyUp(KeyCode.S))
            {
                isSkilling();
            }
            else if (Input.GetKey(KeyCode.W) && !UseSkill)
            {
                isMove = true;
                transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                Playerani.SetBool("isMove", isMove);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(transform.up * .5f);
                if (!isTurn)
                {
                    MaxSpeedChange(-4);
                    isTurn = true;
                }
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(transform.up * -.5f);
                if (!isTurn)
                {
                    MaxSpeedChange(-4);
                    isTurn = true;
                }
            }
            else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                MaxSpeedChange(4);
                isTurn = false;
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                ResetAnimation();
            }
            
        }
    }
    public void ResetSpeed()
    {
        Speed = 0;
    }

    public void isNormal(bool state)
    {
        if (state && !isChangeState)
        {
            if (Input.GetKeyUp(KeyCode.B))
            {
                GameSystem.instance.changeModeBackPack();
                isChangeState = true;
               // ResetAnimation();
                return;
            }
            else if(Input.GetKeyUp(KeyCode.X) && npc != null)
            {
                GameSystem.instance.changeModeTalking(npc);
                isChangeState = true;
              //  ResetAnimation();
                return;
            }
            FrameCount++;
            SpeedChange(isMove);
            MouseEvent(isMove);
            KeyboardEvent(isAttack);
            GetRecovery();
        }
    }
    public void isTalking(bool state)
    {
        if (state && !isChangeState)
        {

            if (UISystem.instance.getDialogPanel().GetComponentInChildren<Dialog>().detectTheDialogFinish())
            {
                GameSystem.instance.changeModeShopping();
            }
        }
    }
    public void isOpenBP(bool state)
    {
        if (state && !isChangeState)
        {
            if (Input.GetKeyUp(KeyCode.B))
            {
                GameSystem.instance.changeModeFollowPlayer();
                isChangeState = true;
                return;
            }
        }
    }
    public void isShopping(bool state)
    {
        if (state && !isChangeState)
        {
            if (UISystem.instance.getShopPanel().GetComponentInChildren<shopUI>().detectExitTheShop())
            {
                GameSystem.instance.changeModeFollowPlayer();
            } 
        }
    }
    public void SetNPC(NPC Person)
    {
        npc = Person;
    }
    public void isSkilling()
    {
        if (SkillAvail && GetSkillCost() <= MP)
        {
            ResetAnimation();
            ResetSpeed();
            MPChange(-GetSkillCost());
            UseSkill = true;
            SkillAvail = false;
            Playerani.SetBool("UseSkill", UseSkill);
            switch (this.name)
            {
                case "DogPBR":
                    StartCoroutine(DogKnightSkill());
                    StartCoroutine(SkillCooldown());
                    break;
                case "Avelyn":
                    StartCoroutine(AvelynSkill());
                    StartCoroutine(SkillCooldown());
                    break;
                case "Footman":
                    StartCoroutine(FootmanSkill());
                    StartCoroutine(SkillCooldown());
                    break;
            }
            Playerani.SetBool("UseSkill", UseSkill);
            StartCoroutine(ResetUseSkill());
            CurCooldown = Cooldown;
            SkillParticle.Play();
        }
    }
    public IEnumerator ResetUseSkill()
    {
        switch (this.name)
        {
            case "DogPBR":
                yield return new WaitForSecondsRealtime(0.5f);
                break;
            case "Avelyn":
                yield return new WaitForSecondsRealtime(SkillAnim.length/2);
                break;
            case "Footman":
                yield return new WaitForSecondsRealtime(SkillAnim.length);
                break;
        }
        UseSkill = false;
        Playerani.SetBool("UseSkill", UseSkill);
    }
    public IEnumerator FootmanSkill()
    {
        ATKChange(45);
        MaxSpeedChange(2);
        yield return new WaitForSeconds(5);
        Healing(25);
        yield return new WaitForSeconds(5);
        Healing(25);
        MaxSpeedChange(-2);
        ATKChange(-45);
        SkillParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }
    public IEnumerator AvelynSkill()
    {
        ATK *= 2;
        yield return new WaitForSeconds(SkillAnim.length/2);
        ATK /= 2;
        Healing(MaxHP/5);
        SkillParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }
    public IEnumerator DogKnightSkill()
    {
        MaxSpeedChange(6);
        GetComponent<PlayerSoundSys>().Skill();
        Healing(5);
        yield return new WaitForSeconds(1);
        GetComponent<PlayerSoundSys>().Skill();
        Healing(5);
        yield return new WaitForSeconds(1);
        GetComponent<PlayerSoundSys>().Skill();
        Healing(5);
        yield return new WaitForSeconds(1);
        GetComponent<PlayerSoundSys>().Skill();
        Healing(5);
        yield return new WaitForSeconds(1);
        GetComponent<PlayerSoundSys>().Skill();
        Healing(5);
        yield return new WaitForSeconds(1);
        MaxSpeedChange(-6);
        SkillParticle.Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear);
    }
    public IEnumerator SkillCooldown()
    {
        yield return new WaitForSeconds(Cooldown);
        SkillAvail = true;
    }
    public float GetSkillCost()
    {
        switch (this.name)
        {
            case "DogPBR":
                return 50;
            case "Avelyn":
                return 40;
            case "Footman":
                return 70;
            default:
                return 0 ;
        }
    }
    public void SetCooldown()
    {
        switch (this.name)
        {
            case "DogPBR":
                Cooldown = 9;
                break;
            case "Avelyn":
                Cooldown = 10;
                break;
            case "Footman":
                Cooldown = 18;
                break;
        }
    }
    public float GetCooldown() { return Cooldown; }
    public void MoneyChange(int num)
    {
        if (Money + num < 0) Debug.Log("Not Enough");
        else Money += num;
    }
    public int GetMoney() { return Money; }
    public void MaxSpeedChange(int change)
    {
        MaxSpeed += change;
    }  
    public float GetCurCooldown() { return CurCooldown; }
    public void CheckCooldown()
    {
        CurCooldown = (CurCooldown - Time.deltaTime < 0) ? 0 : (CurCooldown - Time.deltaTime);
    }
    public void isAnimation(bool Active)
    {
        if (Active)
        {
            Speed = 0;
            //if (temp.GetComponent<TimeLineManager>().isTimeLineCompleted()) GameSystem.instance.changeModeFollowPlayer();
        }
    }
    public bool GetisInBoss() { return isInBoss; }
    public void ResetAnything()
    {
        ResetAnimation();
        UseSkill = false;
        Playerani.SetBool("UseSkill",UseSkill);
        ResetSpeed();
        SkillParticle.Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear);
    }
	public void Relive()
    {
        ResetAnything();
        isLive = true;
        isDie = false;
        Playerani.SetBool("isDie",isDie);
        CurrentHP = MaxHP;
        MP = MaxMP;
        CurCooldown = 0;
    }
}
