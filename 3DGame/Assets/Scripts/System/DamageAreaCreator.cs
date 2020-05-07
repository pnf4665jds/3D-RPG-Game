using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAreaCreator : Singleton<DamageAreaCreator>
{
    // 產生傷害區域
    public GameObject AreaPrefab;

    // 產生立方體區域
    public void CreateCubeArea(Vector3 pos, Quaternion rot, Vector3 size, float Damage, float keepTime)
    {
        GameObject obj = Instantiate(AreaPrefab);
        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.tag = "DamageArea";
        DamageArea area = obj.AddComponent<DamageArea>();
        area.Damage = Damage;
        area.GetComponent<BoxCollider>().size = size * 2;
        Destroy(obj, keepTime);
    }

    // 產生球體區域
    public void CreateSphereArea(Vector3 pos, float radius, float Damage, float keepTime)
    {
        GameObject obj = Instantiate(AreaPrefab);
        obj.transform.position = pos;
        obj.tag = "DamageArea";
        DamageArea area = obj.AddComponent<DamageArea>();
        area.Damage = Damage;
        area.GetComponent<SphereCollider>().radius = radius;
        Destroy(obj, keepTime);
    }

    
}
