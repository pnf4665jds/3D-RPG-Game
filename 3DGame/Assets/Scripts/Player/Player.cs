using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator Playerani;
    public bool isAttack;
    public bool isMove;
    public bool isDie;
    private bool isLive;
    private bool isSetMousePos;
    private bool isFree;

    public float MaxSpeed = 12;
    [SerializeField] private float Speed;
    private float MaxHP;
    private float MaxMP;
    private float CurrentHP; 
    private float MP;
    private float ATK;
    private float FrameCount;

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
        isMove = false;
        isDie = false;
        Speed = 0;
        ATK = 50;
        isFree = true;
        FrameCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLive)
        {
            GetState();
            if (isFree)
            {
                FrameCount++;
                SpeedChange(isMove);
                MouseEvent(isMove);
                KeyboardEvent(isAttack);
                GetRecovery(FrameCount);
            }
        }
    }
    public void GetHurt(float damage) {
        CurrentHP -= damage;
        if(CurrentHP <=0)
        {
            ResetAnimation();
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
        isMove = false;
        Playerani.SetBool("isAttack",isAttack);
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
    public void GetRecovery(float Count)
    {
        if(Count > 50)
        {
            CurrentHP = (CurrentHP > MaxHP) ? MaxHP : (CurrentHP += MaxHP * 0.0005f);
            MP = (MP > MaxMP) ? MaxMP : (MP += MaxMP * 0.0005f);
            Count -= 50;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MonsterField")
        {
            PositionUI.GetComponent<PositionMessage>().showMessage(other.gameObject.GetComponent<MonsterField>().GetFieldName());
        }
        else if (other.tag == "Monster")
        {
            if(isAttack) other.gameObject.GetComponentInParent<MonsterInfo>().GetDamage(ATK);
        }
        else if (other.tag == "Organ")
        {
            other.gameObject.GetComponent<GroundMove>().Triggered();
        }
        else if (other.tag == "goldCoin" || other.tag == "silverCoin" || other.tag == "copperCoin" || other.tag == "HealthPotion" || other.tag == "ManaPotion")
        {
            //GameManger.add(other.gameoject);
            Destroy(other.gameObject);
        }
    }
    public bool GetisAttack()
    {
        return isAttack;
    }
	public void SetSpeed(float speed){ Speed = speed;}
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
        if(GameSystem.instance.isPlayerTalking() || GameSystem.instance.isPlayerOpenBackPack())
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
        if (Input.GetMouseButton(0) && !isMove) SetAttack(true);            
        else if (Input.GetMouseButton(1)) this.transform.GetChild(2).transform.Rotate(-Input.GetAxis("Mouse Y"), 0, 0);
        if (Input.GetMouseButtonUp(0)) ResetAnimation();
    }
    public void SetAttack(bool isAttack)
    {
        this.isAttack = isAttack;
        Playerani.SetBool("isAttack", isAttack);
    }
    public void KeyboardEvent(bool isAttack)
    {
        if (!isAttack)
        {
            if (Input.GetKey(KeyCode.W))
            {
                isMove = true;
                transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                Playerani.SetBool("isMove", isMove);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(transform.up * 1.5f);
                MaxSpeed = 8;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(transform.up * -1.5f);
                MaxSpeed = 8;
            }
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) MaxSpeed = 12;
            if (Input.GetKeyUp(KeyCode.W))
            {
                ResetAnimation();
            }
        }
    }
}
