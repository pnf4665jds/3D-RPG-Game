using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionBase : MonoBehaviour
{
    public abstract void Init();
    public abstract void Process();
    public abstract void Exit();

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
