﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // 這個Script是負責控制掛在機器人身上的防護罩
    // 破壞所有能量物件->防護罩消失

    public List<Transform> Points;      // 能量物件位置
    public GameObject ShieldObject;     // 防護罩物件
    public GameObject EnergyObject;     // 能量物件
    public GameObject RespawnEffect;
    public string RecoverStateName;     // 恢復能量點的狀態名稱
    public float CreateDelay;           // 產生能量點的延遲
    public int EnergyPointNum { get; set; } = 0;
    public bool IsShieldBreak { get; set; } = false;
    //public string 

    private MonsterInfo info;
    private ActionController controller;

    private void Start()
    {
        info = GetComponent<MonsterInfo>();
        controller = GetComponent<ActionController>();
        StartCoroutine(CreateEnergyPoint());
    }

    /// <summary>
    /// 創建能量點在指定位置
    /// </summary>
    public IEnumerator CreateEnergyPoint()
    {
        IsShieldBreak = false;
        info.isInvincible = true;
        ShieldObject.SetActive(true);
        foreach (Transform t in Points)
        {
            GameObject effectObj = Instantiate(RespawnEffect, t.position, Quaternion.identity);
            Destroy(effectObj, 6);
        }
        yield return new WaitForSeconds(CreateDelay);
        foreach (Transform t in Points)
        {
            GameObject energyObj = Instantiate(EnergyObject, t.position, Quaternion.identity);
            energyObj.GetComponent<MeshRenderer>().material.mainTexture = Fill(new Color(0, 0, 1, 0.5f));
            energyObj.GetComponent<EnergyPoint>().Shield = this;
            EnergyPointNum++;
        }
        StartCoroutine(WaitObjectDestroy());
    }

    /// <summary>
    /// 等待能量點破壞
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitObjectDestroy()
    {
        while(EnergyPointNum > 0)
        {
            yield return null;
        }
        info.isInvincible = false;
        IsShieldBreak = true;
        ShieldObject.SetActive(false);
        StartCoroutine(WaitToRecoverShield());
    }

    /// <summary>
    /// 等待BOSS到特定狀態後護盾恢復
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitToRecoverShield()
    {
        yield return new WaitUntil(() => controller.CurrentStateName == RecoverStateName);
        StartCoroutine(CreateEnergyPoint());
    }

    /// <summary>
    /// 能量點上色
    /// </summary>
    public Texture2D Fill(Color clr)
    {
        Color color;
        Texture2D texture = new Texture2D(128, 128);
        int y = 0;

        while (y < texture.height)
        {
            int x = 0;
            while (x < texture.width)
            {
                if (x <= 9 || y <= 9 || x >= 118 || y >= 118)
                    color = new Color(0.5f, 0, 1, 0.8f);
                else
                    color = clr;
                texture.SetPixel(x, y, color);
                ++x;
            }
            ++y;
        }
        texture.Apply();
        return texture;
    }
}