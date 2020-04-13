using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    /// <summary>
    /// 這個script用來管理這項怪物的Action狀態
    /// </summary>
    
    // 當前狀態
    public string CurrentStateName;
    // 當前目標
    public GameObject CurrentTarget;
    // 第一個狀態名稱
    public string FirstStateName;

    [Header("State List")]
    // 狀態列表
    public List<State> StateList;

    private State currentState;

    private void Awake()
    {
        ChangeState(FirstStateName);
    }

    private void Update()
    {
        if (currentState != null)
        {
            // Update目前的Action
            currentState.Action?.Process();
            currentState.EvalTransition();
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
                CurrentTarget = null;
                currentState = s;
                currentState.controller = this;
                currentState.Action?.Init();
                break;
            }
        }
        
    }
}