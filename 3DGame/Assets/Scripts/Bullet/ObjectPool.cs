using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;   // 欲生成的物件
    public int InitSize;    // 初始的生成數量

    private Queue<GameObject> objPool = new Queue<GameObject>();

    private void Awake()
    {
        for(int i = 0; i < InitSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            objPool.Enqueue(obj);
            obj.SetActive(false);
        }
    }
}
