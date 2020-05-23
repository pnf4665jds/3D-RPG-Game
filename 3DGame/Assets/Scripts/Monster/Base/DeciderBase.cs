using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DeciderBase : MonoBehaviour
{
    // 子類別需實作這個函式
    public abstract bool Decide();

    public string UseStateName;     // 這個Decider使用在哪個state
    public float DetectPauseTime = 0;     // 偵測到後幾秒內不再次偵測，可以想像成是冷卻時間

    protected ActionController controller { get; set; }
    protected MonsterInfo monsterInfo { get; set; }

    private bool PauseTimeFinish = true;

    private void Start()
    {
        controller = GetComponent<ActionController>();
        monsterInfo = GetComponent<MonsterInfo>();
    }

    public virtual void Init()
    {
        PauseTimeFinish = true;
    }

    /// <summary>
    /// 達成這個Decider並且要離開目前狀態時要做什麼
    /// </summary>
    public virtual void Exit()
    {
        
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

    /// <summary>
    /// 開始暫停計時器
    /// </summary>
    public void StartPauseTimer()
    {
        // 離開這個狀態後，一定時間內Decider暫停
        if (DetectPauseTime > 0 && PauseTimeFinish == true)
            StartCoroutine(PauseTimer());
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
}
