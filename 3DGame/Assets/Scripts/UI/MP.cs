using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MP : MonoBehaviour
{
    [SerializeField]
    private float MaxMp;
    [SerializeField]
    private float curMp;
    private Image MpBar;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        MpBar = this.GetComponent<Image>();
        MaxMp = player.GetComponent<Player>().GetMaxMP();

    }

    // Update is called once per frame
    void Update()
    {
        curMp = player.GetComponent<Player>().GetMP();
        MpBar.fillAmount = curMp / MaxMp;
        //increaseMP();
    }

   
}
