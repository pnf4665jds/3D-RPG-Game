using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moneySystem : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform MoneyPref;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createMoney(Vector3 moneyPos , int number) {
        for (int i = 0; i < number; i++) {
            Instantiate(MoneyPref, moneyPos, Quaternion.identity);
        }
    }
}
