using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CD : MonoBehaviour
{
    [SerializeField]
    private float MaxCD;
    [SerializeField]
    private float curCD;
    private Image CDBar;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        CDBar = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MaxCD > 0) {
            curCD = player.GetComponent<Player>().GetCurCooldown();
            CDBar.fillAmount = (MaxCD - curCD) / MaxCD;
        }

        
    }
    public void setMaxCD(int Cd) {
        MaxCD = Cd;
        curCD = Cd;

    }
}
