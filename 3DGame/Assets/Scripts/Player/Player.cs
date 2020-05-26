using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator Playerani;
    public bool isAttack;
    public bool isMove;
    public bool isDie;
    public bool UseSkill;
    private bool isLive;
    private bool isFree;
    private bool isChangeState;
    private bool isTurn = false;
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
    [SerializeField]private float CurCooldown;
    [SerializeField]private int Money;

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
        isAttack = false;
        isMove = false;
        isDie = false;
        UseSkill = false;
        Speed = 0;
        ATK = 50;
        isFree = true;
        isChangeState = false;
        SkillAvail = true;
        SetCooldown();
        FrameCount = 0;
        CurCooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLive)
        {
            isChangeState = false;
            isTalking(GameSystem.instance.isPlayerTalking());
            isShopping(GameSystem.instance.isPlayerShopping());
            isOpenBP(GameSystem.instance.isPlayerOpenBackPack());
            isNormal(GameSystem.instance.isPlayerNormal());
        }
        CheckCooldown();
    }
    public void GetHurt(float damage) {
        CurrentHP -= damage;
        if (CurrentHP <= 0)
        {
            ResetAnimation();
            isLive = false;
            isDie = true;
            Playerani.SetBool("isDie", isDie);
        }
    }
    public void Healing(float healing)
    {
        CurrentHP = ((CurrentHP+healing) >= MaxHP) ? MaxHP : (CurrentHP += healing);
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
            AddMP(MaxMP*0.03f);
            FrameCount -= 100;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MonsterField")
        {
            PositionUI.GetComponent<PositionMessage>().showMessage(other.gameObject.GetComponent<MonsterField>().GetFieldName());
        }
        else if (other.tag == "Monster" || other.tag == "Boss")
        {
            if (isAttack) other.gameObject.GetComponentInParent<MonsterInfo>().GetDamage(ATK);
        }
        else if (other.tag == "Organ")
        {
            other.gameObject.GetComponent<GroundMove>().Triggered();
        }
        else if (other.tag == "goldCoin" || other.tag == "silverCoin" || other.tag == "copperCoin" || other.tag == "HealthPotion" || other.tag == "ManaPotion")
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
            }
            Destroy(other.gameObject);
        }
    }
    public bool GetisAttack()
    {
        return isAttack;
    }
    public void SetSpeed(float speed) { Speed = speed; }
    public float GetSpeed() { return Speed; }
    public void SetisFree(bool free) { isFree = free; }
    public bool GetisFree() { return isFree; }
    public void SpeedChange(bool isMove)
    {
        if (isMove) Speed = (Speed > MaxSpeed) ? MaxSpeed : (Speed += Time.deltaTime * MaxSpeed);
        else Speed = 0;
    }
    public void GetState()
    {
        if (GameSystem.instance.isPlayerTalking() || GameSystem.instance.isPlayerOpenBackPack())
        {

            isFree = false;
        }
        else if (GameSystem.instance.isPlayerNormal())
        {
            isFree = true;
        }
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
    public void AddMP(float num)
    {
        MP = (MP > MaxMP) ? MaxMP : (MP += num);
    }
    public void KeyboardEvent(bool isAttack)
    {
        if (!isAttack)
        {
            if (Input.GetKey(KeyCode.W))
            {
                isMove = true;
                transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                //Cc.SimpleMove(transform.forward * Speed * Time.deltaTime);
                Playerani.SetBool("isMove", isMove);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(transform.up * 1.5f);
                if (!isTurn)
                {
                    MaxSpeedChange(-4);
                    isTurn = true;
                }
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(transform.up * -1.5f);
                if (!isTurn)
                {
                    MaxSpeedChange(-4);
                    isTurn = true;
                }
            }
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                MaxSpeedChange(4);
                isTurn = false;
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                ResetAnimation();
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                isSkilling(); 
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
                return;
            }
            else if(Input.GetKeyUp(KeyCode.X) && npc != null)
            {
                GameSystem.instance.changeModeTalking(npc);
                isChangeState = true;
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
            AddMP(-GetSkillCost());
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
        }
    }
    public IEnumerator ResetUseSkill()
    {
        yield return new WaitForSeconds(0.5f);
        UseSkill = false;
        Playerani.SetBool("UseSkill", UseSkill);

    }
    public IEnumerator FootmanSkill()
    {
        ATKChange(25);
        yield return new WaitForSeconds(10);
        ATKChange(-25);
    }
    public IEnumerator AvelynSkill()
    {
        ATK *= 2;
        yield return new WaitForSeconds(1);
        ATK /= 2;
        Healing(MaxHP/4);
    }
    public IEnumerator DogKnightSkill()
    {
        MaxSpeedChange(4);
        yield return new WaitForSeconds(1);
        Healing(5);
        yield return new WaitForSeconds(1);
        Healing(5);
        yield return new WaitForSeconds(1);
        Healing(5);
        yield return new WaitForSeconds(1);
        Healing(5);
        yield return new WaitForSeconds(1);
        MaxSpeedChange(-4);
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
                return 30;
            case "Footman":
                return 40;
            default:
                return 0 ;
        }
    }
    public void SetCooldown()
    {
        switch (this.name)
        {
            case "DogPBR":
                Cooldown = 10;
                break;
            case "Avelyn":
                Cooldown = 8;
                break;
            case "Footman":
                Cooldown = 14;
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
}
