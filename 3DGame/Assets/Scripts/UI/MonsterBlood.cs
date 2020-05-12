using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBlood : MonoBehaviour
{
    private float MaxBlood;
    private float curBlood; //get the monster who get hurt
    private GameObject player;
    private GameObject bloodUI;
    private Sprite monsterImage;
    private string monsterName;
    
    /// <summary>
    /// 如果血條X軸座標移動至-92.6判定死亡
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        //bloodUI.SetActive(false);
        player = GameObject.FindWithTag("Player");
        /*MaxBlood = 100;
        curBlood = 100;*/
    }

    // Update is called once per frame
    void Update()
    {
        if (MaxBlood > 0) {
            this.transform.localPosition = new Vector3((-92.6f + 92.6f * (curBlood / MaxBlood)), 0.0f, 0.0f);
        }
        
           
    }
    private void ChangeColor()
    {

        if (curBlood <= 20)
        {
            this.GetComponent<Image>().color = Color.red;
        }
        else this.GetComponent<Image>().color = Color.white;

    }
    public void setMaxBlood(float blood) {
        MaxBlood = blood;
        curBlood = MaxBlood;
    }
    private void setCurBlood(int blood) {
        curBlood = blood;
    }
    private void setMonsterImage(string monsterName) {
        monsterImage = Resources.Load<Sprite>("MonsterImages" + "/" + monsterName);
    }
}
