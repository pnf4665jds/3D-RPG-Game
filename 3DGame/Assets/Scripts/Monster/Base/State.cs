using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class State
{
    public string StateName;
    public ActionBase Action;
    public List<Transition> TransList;
    public ActionController controller { get; set; }

    // 判斷是否要轉換狀態
    public void EvalTransition()
    {
        if (controller.CurrentStateName != StateName)
            return;

        foreach (Transition t in TransList)
        {
            if (t.Decider.Decide())
            {
                // 如果判斷用script回傳true
                if (t.TrueState != "" && t.Decider.IsPauseTimeFin())
                {
                    t.Decider.Exit();
                    controller.ChangeState(t.TrueState);
                }
            }
            else
            {
                // 如果判斷用script回傳false
                if (t.FalseState != "" && t.Decider.IsPauseTimeFin())
                {
                    t.Decider.Exit();
                    controller.ChangeState(t.FalseState);
                }
            }
        }
    }
}

/// <summary>
/// Decider:決定狀態轉移的script，如果True就轉成TrueState，反之為FalseState
/// </summary>
[System.Serializable]
public class Transition
{
    public int Order;
    public DeciderBase Decider;
    public string TrueState;
    public string FalseState;
}
