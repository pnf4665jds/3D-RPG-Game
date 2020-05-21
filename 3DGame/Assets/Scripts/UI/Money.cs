using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private int totalMoney ;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        totalMoney = player.GetComponent<Player>().GetMoney();
        this.GetComponent<Text>().text = totalMoney.ToString();
    }
}
