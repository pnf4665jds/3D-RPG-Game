using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class deadUI : MonoBehaviour
{
    public Button use;
    public Button notUse;
    public Sprite RecoveryPotion;
    // Start is called before the first frame update
    void Start()
    {
        use.gameObject.SetActive(false);
        notUse.gameObject.SetActive(false);
        StartCoroutine(FindPotion());
        use.onClick.AddListener(clickUse);
        notUse.onClick.AddListener(exit);
    }

    public IEnumerator FindPotion() {
        this.transform.GetChild(1).GetComponent<Text>().text = "";

        yield return new WaitUntil(() => GameSystem.instance.isPlayerDead());
        this.transform.GetChild(1).GetComponent<Text>().text = "檢測到玩家生命值過低" ;
        yield return new WaitForSecondsRealtime(1);
        this.transform.GetChild(1).GetComponent<Text>().text = "檢測到玩家生命值過低\n搜尋復活藥水中 . . .";
        yield return new WaitForSecondsRealtime(1);
        this.transform.GetChild(1).GetComponent<Text>().text = "檢測到玩家生命值過低\n搜尋復活藥水中 . . .\n. . .";
        yield return new WaitForSecondsRealtime(1);
        this.transform.GetChild(1).GetComponent<Text>().text = "檢測到玩家生命值過低\n搜尋復活藥水中 . . .\n. .";
        yield return new WaitForSecondsRealtime(1);
        this.transform.GetChild(1).GetComponent<Text>().text = "檢測到玩家生命值過低\n搜尋復活藥水中 . . .\n. ";
        yield return new WaitForSecondsRealtime(1);
        this.transform.GetChild(1).GetComponent<Text>().text = "檢測到玩家生命值過低\n搜尋復活藥水中 . . .\n. .";
        yield return new WaitForSecondsRealtime(1);
        this.transform.GetChild(1).GetComponent<Text>().text = "檢測到玩家生命值過低\n搜尋復活藥水中 . . .\n. . .";
        yield return new WaitForSecondsRealtime(1);
        if (!Inventory.instance.GetComponent<Inventory>().detectEmptyPotion(RecoveryPotion))
        {
            StartCoroutine(havePotion());
        }
        else
        {
            StartCoroutine(zeroPotion());
        }
        


    }
    public IEnumerator havePotion() {

        this.transform.GetChild(1).GetComponent<Text>().text = "搜尋復活藥水中 . . .\n搜索成功，請選擇是否使用。";
        use.gameObject.SetActive(true);
        notUse.gameObject.SetActive(true);
        yield return null;

    }
    public IEnumerator zeroPotion()
    {

        this.transform.GetChild(1).GetComponent<Text>().text = "搜尋復活藥水中 . . .\n搜索失敗，將於一秒後離開。";

        yield return null;

    }
    public void clickUse()
    {

    }
    public void exit() {

    }
}
