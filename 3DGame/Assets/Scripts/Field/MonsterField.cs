using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterField : MonoBehaviour
{
    public GameObject Monster;     // 怪物Prefab
    public int MonsterNum;  // 怪物數量
    public string Name;
    
    public float radius {
        get {
            return this.GetComponent<SphereCollider>().radius;
        }
    }

    public Vector3 center
    {
        get
        {
            return this.transform.position;
        }
    }

    void Start()
    {
        CreateMonster();
    }
    public string GetFieldName() {
        return Name;
    }
    public float GetFieldRadius() {
        return radius;
    }

    /// <summary>
    /// 怪物生成
    /// </summary>
    public void CreateMonster()
    {
        float squareWidth = Mathf.Sqrt(2 * Mathf.Pow(radius, 2));
        squareWidth = Mathf.Floor(squareWidth); // 取整數
        float startX = center.x - squareWidth / 2;
        float startZ = center.z - squareWidth / 2;
        
        for(int i = 0; i < MonsterNum; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(startX, startX + squareWidth), center.y, Random.Range(startZ, startZ + squareWidth));
            GameObject obj = Instantiate(Monster, randomPos, Quaternion.identity, gameObject.transform);
            obj.GetComponent<MonsterInfo>().SetField(center, radius);
        }
    }
}
