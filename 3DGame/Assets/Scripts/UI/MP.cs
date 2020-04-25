using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MP : MonoBehaviour
{
    private float MaxMp;
    private float curMp;
    private Image MpBar;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("player");
        MpBar = this.GetComponent<Image>();
        MaxMp = 100.0f;

    }

    // Update is called once per frame
    void Update()
    {
       // curMp = player.GetComponent<Player>().
        MpBar.fillAmount = curMp / MaxMp;
        increaseMP();
    }

    public void increaseMP()
    {
        if (/*沒有在利用技能*/curMp < MaxMp)
        {
            curMp += 0.01f;
        }
    }
    public void decreaseMP(float deMP)
    {
        curMp -= deMP;
    }
}
