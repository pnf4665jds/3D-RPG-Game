using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    public float MaxBlood;
    public float curBlood;
    /// <summary>
    /// 如果血條X軸座標移動至-92.6判定死亡
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        MaxBlood = 100;
        curBlood = 100;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = new Vector3( (-92.6f + 92.6f * (curBlood / MaxBlood)) , 0.0f  ,0.0f);
    }
}
