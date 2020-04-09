using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DeciderBase : MonoBehaviour
{
    public abstract bool Decide();
    // 這個判斷script使用在哪個state
    public string UseStateName;

    protected ActionController controller { get; set; }
    protected MonsterInfo monsterInfo { get; set; }

    private void Start()
    {
        controller = GetComponent<ActionController>();
        monsterInfo = GetComponent<MonsterInfo>();
    }
}
