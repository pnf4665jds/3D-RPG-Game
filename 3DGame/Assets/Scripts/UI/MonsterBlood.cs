using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBlood : MonoBehaviour
{
    public float MaxBlood;
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
        bloodUI = this.transform.parent.parent.gameObject;
        bloodUI.SetActive(false);
        player = GameObject.FindWithTag("Player");
        MaxBlood = 100;
        curBlood = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (/**monster get hurt**/true) {
            //player.GetComponent<Player>()
            //bloodUI.SetActive(true) ;

            this.transform.localPosition = new Vector3((-92.6f + 92.6f * (curBlood / MaxBlood)), 0.0f, 0.0f);
            ChangeColor();
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
    private void setCurBlood(int blood) {
        curBlood = blood;
    }
    private void setMonsterImage(string monsterName) {
        monsterImage = Resources.Load<Sprite>("MonsterImages" + "/" + monsterName);
    }
}
