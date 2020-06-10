using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSystem : Singleton<MonsterSystem>
{
    public Dictionary<int, MonsterInfo> MonsterList { get; set; } = new Dictionary<int, MonsterInfo>();
    public bool IsBossDead { get; set; }    // 回傳Boss是否死亡
    public int MonsterNum { get; set; } = 0;    // 小怪累積數量
    public int BossNum { get; set; } = 0;       // Boss累積數量
    public int DeadMonsterNum { get; set; } = 0;    // 死亡的小怪累積數量
    public int DeadBossNum { get; set; } = 0;   // 死亡的Boss累積數量

    private int monsterId = -1;

    /// <summary>
    /// 註冊一個怪物到MonsterList，並回傳其註冊Id
    /// </summary>
    /// <returns></returns>
    public int AddMonster(MonsterInfo info)
    {
        monsterId++;
        MonsterList.Add(monsterId, info);
        if (info.gameObject.tag == "Boss")
        {
            IsBossDead = false;
            BossNum++;
        }
        else
        {
            MonsterNum++;
        }

        return monsterId;
    }

    /// <summary>
    /// 移除指定Id的怪物
    /// </summary>
    /// <param name="id"></param>
    public void RemoveMonster(int id)
    {
        if (MonsterList.ContainsKey(id))
        {
            if (MonsterList[id].gameObject.tag == "Boss")
            {
                IsBossDead = true;
                DeadBossNum++;
            }
            else
            {
                DeadMonsterNum++;
            }

            MonsterList.Remove(id);
        }
    }

    /// <summary>
    /// 移除所有怪物
    /// </summary>
    public void RemoveAllMonster()
    {
        monsterId = -1;
        MonsterList.Clear();
    }

    /// <summary>
    /// 讓所有怪物在播放動畫狀態下Idle
    /// </summary>
    public void IdleAllMonsterOnAnimation()
    {
        for(int i = 0; i <= monsterId; i++)
        {
            if (MonsterList.ContainsKey(i))
            {
                StartCoroutine(MonsterList[i].IdleOnAnimationState());
            }
        }
    }

    /// <summary>
    /// Enable或Disable怪物的ActionController
    /// </summary>
    /// <param name="isActive"></param>
    public void SetMonsterActionController(int id, bool isStop)
    {
        ActionController controller = MonsterList[id].GetComponent<ActionController>();
        if (isStop == true)
        {
            controller.Stop = false;
            controller.InitAllDecider();
            controller.ChangeState(controller.StateNameAfterPlayerDead == "" ? controller.CurrentStateName : controller.StateNameAfterPlayerDead, 0);
        }
        else
        {
            controller.Stop = true;
            controller.ExitCurrentAction();
        }
    }

    /// <summary>
    /// 取得Boss的MonsterInfo
    /// </summary>
    /// <returns></returns>
    public MonsterInfo GetBossInfo()
    {
        for (int i = 0; i <= monsterId; i++)
        {
            if (!MonsterList.ContainsKey(i))
                continue;
            if (MonsterList[i].gameObject.tag == "Boss")
                return MonsterList[i];
        }

        return null;
    }
}
