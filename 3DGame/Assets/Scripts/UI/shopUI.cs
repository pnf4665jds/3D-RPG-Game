using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopUI : MonoBehaviour
{
    public TextAsset stregnthDetail;
    public TextAsset healthDetail;
    public TextAsset manaDetail;
    public Button strength;
    public Button health;
    public Button mana;
    public Button Buy;
    public Button Exit;
    private bool isBuy;
    private bool ExitShop;
    private void Start()
    {
        this.transform.GetChild(3).GetComponentInChildren<Text>().text = "歡迎光臨，請問需要購買什麼";
        this.transform.GetChild(4).GetComponentInChildren<Text>().text = "來買來買 ! ! !";
        Buy.gameObject.SetActive(false);
        isBuy = false;
        ExitShop = false;
    }
    private void Update()
    {
        strength.onClick.AddListener(clickStrength);
        health.onClick.AddListener(clickHealth);
        mana.onClick.AddListener(clickMana);
        Buy.onClick.AddListener(clickBuy);
        Exit.onClick.AddListener(clickExit);

    }
    private void clickStrength() {
        this.transform.GetChild(3).GetComponentInChildren<Text>().text = stregnthDetail.text;
        this.transform.GetChild(4).GetComponentInChildren<Text>().text = "喔喔 !!! 這個可能比較貴，一次要10000塊錢 (ﾟ3ﾟ)～♪";
        Buy.gameObject.SetActive(true);
    }
    private void clickHealth()
    {
        this.transform.GetChild(3).GetComponentInChildren<Text>().text = healthDetail.text;
        this.transform.GetChild(4).GetComponentInChildren<Text>().text = "喔喔 !!! 這個有時會放在寶箱裡，一瓶要1000塊錢 (ﾟ3ﾟ)";
        Buy.gameObject.SetActive(true);
        
    }
    private void clickMana()
    {
        this.transform.GetChild(3).GetComponentInChildren<Text>().text = manaDetail.text;
        this.transform.GetChild(4).GetComponentInChildren<Text>().text = "喔喔 !!! 這個有時會放在寶箱裡，一瓶要2000塊錢 (ﾟ3ﾟ)";
        Buy.gameObject.SetActive(true);
    }
    private void clickBuy() {
        this.transform.GetChild(4).GetComponentInChildren<Text>().text = "謝謝惠顧";
        isBuy = true;
    }
    private void clickExit() {
        if (!isBuy) { this.transform.GetChild(4).GetComponentInChildren<Text>().text = "都沒有買東西 QQ，期待下次再來 !!!"; ; }
        else { this.transform.GetChild(4).GetComponentInChildren<Text>().text = "感謝消費，希望我能在之後也能看到你 !!!"; }
        ExitShop = true;
    }
    public bool detectExitTheShop() {
        return ExitShop;
    }

}
