using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionBase : MonoBehaviour
{
    // 子類別必須實作以下三個函式
    public abstract void Init();
    public abstract void Process();        
    public abstract void Exit();

    public string UseStateName;     // 這個Action使用在哪個state

    protected ActionController controller { get; set; }
    protected Animator animator { get; set; }
    protected MonsterInfo monsterInfo { get; set; }

    private void Start()
    {
        controller = GetComponent<ActionController>();
        animator = GetComponent<Animator>();
        monsterInfo = GetComponent<MonsterInfo>();
    }
}
