using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class deadUI : MonoBehaviour
{
    public Button use;
    public Button notUse;
    public Sprite RecoveryPotion;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        use.gameObject.SetActive(false);
        notUse.gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        use.onClick.AddListener(clickUse);
        notUse.onClick.AddListener(exit);
    }

    public IEnumerator FindPotion() {

        this.transform.GetChild(1).GetComponent<Text>().text = "";
        use.gameObject.SetActive(false);
        notUse.gameObject.SetActive(false);
        //yield return new WaitUntil(() => GameSystem.instance.isPlayerDead());
        this.transform.GetChild(1).GetComponent<Text>().text = "檢測到玩家生命值過低" ;
        yield return new WaitForSeconds(1);
        this.transform.GetChild(1).GetComponent<Text>().text = "檢測到玩家生命值過低\n搜尋復活藥水中 . . .";
        yield return new WaitForSeconds(1);
        this.transform.GetChild(1).GetComponent<Text>().text = "檢測到玩家生命值過低\n搜尋復活藥水中 . . .\n. . .";
        yield return new WaitForSeconds(1);
        this.transform.GetChild(1).GetComponent<Text>().text = "檢測到玩家生命值過低\n搜尋復活藥水中 . . .\n. .";
        yield return new WaitForSeconds(1);
        this.transform.GetChild(1).GetComponent<Text>().text = "檢測到玩家生命值過低\n搜尋復活藥水中 . . .\n. ";
        yield return new WaitForSeconds(1);
        if (!Inventory.instance.GetComponent<Inventory>().detectEmptyPotion(RecoveryPotion))
        {
            havePotion();
        }
        else
        {
            zeroPotion();
        }
        


    }
    public void havePotion() {

        this.transform.GetChild(1).GetComponent<Text>().text = "搜尋復活藥水中 . . .\n搜索成功，請選擇是否使用。";
        use.gameObject.SetActive(true);
        notUse.gameObject.SetActive(true);


    }
    public void zeroPotion()
    {

        this.transform.GetChild(1).GetComponent<Text>().text = "搜尋復活藥水中 . . .\n搜索失敗，將於一秒後離開。";


    }
    public void clickUse()
    {
        GameSystem.instance.changeModeFollowPlayer();
        player.GetComponent<Player>().Relive();
    }
    public void exit() {

    }
}
