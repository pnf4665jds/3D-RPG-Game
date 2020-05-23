using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class State
{
    public string StateName;    // 狀態名稱
    public ActionBase Action;   // 這個狀態下要做的事
    public List<Transition> TransList;  // 狀態轉移列表
    public ActionController controller { get; set; }

    private bool hasEval = false;

    public void Init()
    {
        hasEval = false;
    }

    // 判斷是否要切換狀態
    public void EvalTransition()
    {
        // 如果目前狀態不是這個狀態
        if (controller.CurrentStateName != StateName)
            return;

        foreach (Transition t in TransList)
        {
            // 如果Decider回傳true
            if (t.Decider.Decide())
            {
                // 如果有設定新狀態，而且這個狀態的Decider暫停時間結束了
                if (t.TrueState != "" && t.Decider.IsPauseTimeFin() && !hasEval)
                {
                    hasEval = true;
                    t.Decider.StartPauseTimer();
                    controller.ChangeState(t.TrueState);
                }
            }
            else
            {
                // 同上
                if (t.FalseState != "" && t.Decider.IsPauseTimeFin() && !hasEval)
                {
                    hasEval = true;
                    t.Decider.StartPauseTimer();
                    controller.ChangeState(t.FalseState);
                }
            }
        }
    }
}

/// <summary>
/// Decider:決定狀態轉移的script，如果Decider回傳True就轉成TrueState，反之為FalseState
/// </summary>
[System.Serializable]
public class Transition
{
    public int Order;   // 判斷優先度，數字越小優先度越高
    public DeciderBase Decider;
    public string TrueState;    
    public string FalseState;
}
