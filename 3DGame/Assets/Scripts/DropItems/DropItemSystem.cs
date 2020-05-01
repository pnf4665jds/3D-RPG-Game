using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemSystem : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject MoneyPref;
    public GameObject HealthPref;
    public GameObject ManaPref;

    public void createMoney(Vector3 pos , int number) {

        for (int i = 0; i < number; i++) {
            Instantiate(MoneyPref, pos, Quaternion.identity);
        }

    }
    public void createHealthItem(Vector3 pos, int number)
    {

        for (int i = 0; i < number; i++)
        {
            Instantiate(HealthPref, pos, Quaternion.identity);
        }

    }
    public void createManaItem(Vector3 pos, int number)
    {

        for (int i = 0; i < number; i++)
        {
            Instantiate(ManaPref, pos, Quaternion.identity);
        }

    }
}
