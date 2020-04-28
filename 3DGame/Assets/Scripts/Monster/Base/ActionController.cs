using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    // 這個script用來管理這個怪物的Action狀態

    public string CurrentStateName;     // 當前狀態
    public string FirstStateName;       // 第一個狀態名稱

    [Header("State List")]
    public List<State> StateList;       // 狀態列表

    private State currentState;

    private void Awake()
    {
        ChangeState(FirstStateName);
        OrderingList();
    }

    private void Start()
    {
        InitAllDecider();
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.Action?.Process();     // Update目前狀態的Action的Process函式
            currentState.EvalTransition();      // Update判斷是否需要切換狀態
        }
    }

    /// <summary>
    /// 使Transition List可以透過設定Order改變判斷優先度，便於編輯。數字越小優先度越高。
    /// </summary>
    private void OrderingList()
    {
        foreach (State s in StateList)
        {
            // 以Order進行排序
            s.TransList.Sort((t1, t2) => t1.Order.CompareTo(t2.Order));
        }
    }

    /// <summary>
    /// 切換狀態
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeState(string newStateName)
    {
        CurrentStateName = newStateName;
        foreach (State s in StateList)
        {
            if (s.StateName == newStateName)
            {
                currentState?.Action?.Exit();
                currentState = s;
                currentState.controller = this;
                currentState.Action?.Init();
                break;
            }
        }

    }

    /// <summary>
    /// 初始化所有Decider
    /// </summary>
    public void InitAllDecider()
    {
        StateList.ForEach(s => s.TransList.ForEach(t => t.Decider.Init()));
    }
}