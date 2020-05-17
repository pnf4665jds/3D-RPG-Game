using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;

public class ActionStoringEnergy : ActionBase
{
    public float StoringTime;
    public GameObject ShootPosition;
    public GameObject StoringObject;    // 能量儲存特效1
    public GameObject StoringBall;      // 能量儲存特效2
    public GameObject LightningEffect;   // 閃電特效
    public List<BoltPoints> BoltPointsList;

    private GameObject storingObject;
    private GameObject storingBall;
    private GameObject target;

    public override void Init()
    {
        target = GameObject.FindWithTag("Player");
        storingObject = Instantiate(StoringObject, transform.position + Vector3.up, Quaternion.identity);
        storingBall = Instantiate(StoringBall, ShootPosition.transform.position, Quaternion.identity, transform);
        foreach (BoltPoints points in BoltPointsList)
        {
            LightningBoltScript lightning = Instantiate(LightningEffect).GetComponent<LightningBoltScript>();
            lightning.StartObject = points.StartPos;
            lightning.EndObject = points.EndPos;
        }
    }

    public override void Process()
    {
        storingObject.transform.localScale -= new Vector3(15f, 0, 15f) / StoringTime * Time.deltaTime;
        AimTarget();
    }

    public override void Exit()
    {
        Destroy(storingObject);
        Destroy(storingBall);
    }

    private void AimTarget()
    {
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, target.transform.position - transform.position, monsterInfo.RotateSpeed * Time.deltaTime, 0.0f);
        newDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}

[System.Serializable]
public class BoltPoints
{
    public GameObject StartPos;
    public GameObject EndPos;
}
