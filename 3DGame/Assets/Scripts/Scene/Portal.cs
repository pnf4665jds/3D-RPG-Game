using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider))]
public class Portal : MonoBehaviour
{
    // 這個script負責控制角色傳送

    public string SceneName;        // 傳送場景
    public Vector3 DestPosition;    // 傳送後座標
    public float DestRotationY;   // 傳送後Y軸旋轉
    public PortalCondition Condition;   // 傳送條件

    private List<bool> ConditionMeetList = new List<bool>();

    private void Awake()
    {
        for (int i = 0; i < Enum.GetNames(typeof(PortalCondition)).Length; i++)
            ConditionMeetList.Add(false);
    }

    /// <summary>
    /// 設置條件
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="value"></param>
    public void SetCondition(PortalCondition condition, bool value)
    {
        ConditionMeetList[(int)condition] = value;
    }

    /// <summary>
    /// 是否滿足特定條件
    /// </summary>
    /// <returns></returns>
    private bool ConditionMeet()
    {
        int index = (int)Condition;
        return index == 0 || ConditionMeetList[index] == true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && ConditionMeet())
        {
            GameSceneManager.instance.LoadScene(SceneName, new List<Action>()
            {
                // 切換場景時將主角移到指定座標
                () => other.gameObject.transform.position = DestPosition,
                // 更改成指定的Y軸旋轉角度
                () => other.gameObject.transform.rotation = Quaternion.Euler(0, DestRotationY, 0),
                // 重置狀態
                () => other.GetComponent<Player>().SetSpeed(0)
            });
        }
    }
}

public enum PortalCondition
{
    None = 0,
    BossDead = 1,
    GetItem = 2,
}
