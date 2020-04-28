using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterField : MonoBehaviour
{
    public string Name;
    
    public float radius {
        get {
            return this.GetComponent<SphereCollider>().radius;
        }
    }

    void Start()
    {
        
    }
    public string GetFieldName() {
        return Name;
    }
    public float GetFieldRadius() {
        return radius;
    }

}
