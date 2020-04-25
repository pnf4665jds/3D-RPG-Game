using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DeciderBase : MonoBehaviour
{
    public abstract bool Decide();
    // 這個判斷script使用在哪個state
    public string UseStateName;
    // 偵測到後幾秒內不再次偵測
    public float DetectPauseTime = 0;

    protected ActionController controller { get; set; }
    protected MonsterInfo monsterInfo { get; set; }
    protected bool PauseTimeFinish = true;

    private void Start()
    {
        controller = GetComponent<ActionController>();
        monsterInfo = GetComponent<MonsterInfo>();
    }

    /// <summary>
    /// Decider偵測暫停的計時器
    /// </summary>
    /// <returns></returns>
    private IEnumerator PauseTimer()
    {
        PauseTimeFinish = false;
        yield return new WaitForSeconds(DetectPauseTime);
        PauseTimeFinish = true;
    }

    /// <summary>
    /// 達成這個Decider並且要離開目前狀態時要做什麼
    /// </summary>
    public virtual void Exit()
    {
        // 離開這個狀態後，一定時間內Decider暫停
        if (DetectPauseTime > 0)
            StartCoroutine(PauseTimer());
    }

    /// <summary>
    /// 回傳是否暫停時間已經結束
    /// </summary>
    /// <returns></returns>
    public bool IsPauseTimeFin()
    {
        if (DetectPauseTime <= 0)   // 沒有設置時間
            return true;
        else
            return PauseTimeFinish;
    }
}
