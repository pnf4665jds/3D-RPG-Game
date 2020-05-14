using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;

public class ActionStoringEnergy : ActionBase
{
    public GameObject StoringObject;    // 能量儲存特效
    public GameObject LightningEffect;   // 閃電特效
    public List<BoltPoints> BoltPointsList;

    private GameObject storingObject;

    public override void Init()
    {
        storingObject = Instantiate(StoringObject, transform.position + Vector3.up, Quaternion.identity);
        foreach(BoltPoints points in BoltPointsList)
        {
            LightningBoltScript lightning = Instantiate(LightningEffect).GetComponent<LightningBoltScript>();
            lightning.StartObject = points.StartPos;
            lightning.EndObject = points.EndPos;
        }
    }

    public override void Process()
    {
        storingObject.transform.localScale -= new Vector3(1f, 0, 1f) * Time.deltaTime;
    }

    public override void Exit()
    {

    }
}

[System.Serializable]
public class BoltPoints
{
    public GameObject StartPos;
    public GameObject EndPos;
}
