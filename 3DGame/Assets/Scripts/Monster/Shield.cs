using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // 這個Script是負責控制掛在機器人身上的防護罩
    // 破壞所有能量物件->防護罩消失

    public List<Transform> Points;
    public GameObject ShieldObject;     // 防護罩物件
    public GameObject EnergyObject;     // 能量物件
    public int EnergyPointNum { get; set; } = 0;
    public bool isShieldBreak { get; set; } = false;

    private MonsterInfo info;
    
    private void Start()
    {
        info = GetComponent<MonsterInfo>();
        CreateEnergyPoint();
    }

    /// <summary>
    /// 創建能量點在指定位置
    /// </summary>
    private void CreateEnergyPoint()
    {
        isShieldBreak = false;
        info.isInvincible = true;
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
        isShieldBreak = true;
        ShieldObject.SetActive(false);
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