using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemSystem : Singleton<DropItemSystem>
{
    // Start is called before the first frame update

    public GameObject GoldCoinPref;
    public GameObject SilverCoinPref;
    public GameObject CopperCoinPref;
    public GameObject HealthPref;
    public GameObject ManaPref;
    public GameObject RecoveryPref;

    public Sprite HealthPotion;
    public Sprite ManaPotion;
    public Sprite RecoveryPotion;

    /// <summary>
    /// 產生金幣。
    /// </summary>
    /// <param name="pos">生成座標</param>
    /// <param name="number">總金額</param>
    public void createMoney(Vector3 pos , int sum) {

        GameObject coin;
        Vector3 delta;
        while (sum / 1000 > 0)
        {
            delta = new Vector3(Random.Range(-5, 5), 3, Random.Range(-5, 5));
            coin = Instantiate(GoldCoinPref, pos + delta, Quaternion.identity);
            coin.transform.localScale = new Vector3(4, 4 , 4);
            sum -= 1000;
        }
        if (sum / 500 > 0)
        {
            delta = new Vector3(Random.Range(-5, 5), 3, Random.Range(-5, 5));
            coin = Instantiate(SilverCoinPref, pos + delta, Quaternion.identity);
            coin.transform.localScale = new Vector3(4, 4, 4);
            sum -= 500;
        }
        while(sum / 100 > 0)
        {
            delta = new Vector3(Random.Range(-5, 5), 3, Random.Range(-5, 5));
            coin = Instantiate(CopperCoinPref, pos + delta, Quaternion.identity);
            coin.transform.localScale = new Vector3(4, 4, 4);
            sum -= 100;
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
    public void AddPotionToPack(GameObject temp) {
        if (temp == HealthPref)
        {
            Inventory.instance.addItem(HealthPotion);
        }
        else if (temp == ManaPref)
        {
            Inventory.instance.addItem(ManaPotion);
        }
        else if (temp == RecoveryPref)
        {
            Inventory.instance.addItem(RecoveryPotion);
        }

    }

}
