using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionBase : MonoBehaviour
{
    public abstract void Init();
    // 同時間只有一個Process
    public abstract void Process();
    public abstract void Exit();

    // 這個判斷script使用在哪個state
    public string UseStateName;

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
