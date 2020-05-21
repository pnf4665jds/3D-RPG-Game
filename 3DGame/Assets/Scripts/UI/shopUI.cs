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
    public GameObject HealthPotion;
    public GameObject ManaPotion;
    private GameObject player;
    private enum BuyItem{
        none ,
        strengthItem,
        healthItem,
        manaItem

    }
    private BuyItem chooseItem;
    private bool isBuy;
    private bool ExitShop;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        this.transform.GetChild(3).GetComponentInChildren<Text>().text = "歡迎光臨，請問需要購買什麼";
        this.transform.GetChild(4).GetComponentInChildren<Text>().text = "來買來買 ! ! !";
        chooseItem = BuyItem.none;
        Buy.gameObject.SetActive(false);
        isBuy = false;
        ExitShop = false;

        strength.onClick.AddListener(clickStrength);
        health.onClick.AddListener(clickHealth);
        mana.onClick.AddListener(clickMana);
        Buy.onClick.AddListener(clickBuy);
        Exit.onClick.AddListener(clickExit);

    }
    
    private void clickStrength() {
        chooseItem = BuyItem.none;
        this.transform.GetChild(3).GetComponentInChildren<Text>().text = stregnthDetail.text;
        this.transform.GetChild(4).GetComponentInChildren<Text>().text = "喔喔 !!! 這個可能比較貴，一次要10000塊錢 (ﾟ3ﾟ)～♪";
        chooseItem = BuyItem.strengthItem;
        Buy.gameObject.SetActive(true);
    }
    private void clickHealth()
    {
        chooseItem = BuyItem.none;
        this.transform.GetChild(3).GetComponentInChildren<Text>().text = healthDetail.text;
        this.transform.GetChild(4).GetComponentInChildren<Text>().text = "喔喔 !!! 這個有時會放在寶箱裡，一瓶要1000塊錢 (ﾟ3ﾟ)";
        chooseItem = BuyItem.healthItem;
        Buy.gameObject.SetActive(true);
        
    }
    private void clickMana()
    {
        chooseItem = BuyItem.none;
        this.transform.GetChild(3).GetComponentInChildren<Text>().text = manaDetail.text;
        this.transform.GetChild(4).GetComponentInChildren<Text>().text = "喔喔 !!! 這個有時會放在寶箱裡，一瓶要2000塊錢 (ﾟ3ﾟ)";
        chooseItem = BuyItem.manaItem;
        Buy.gameObject.SetActive(true);
    }
    private void clickBuy() {
        if (chooseItem == BuyItem.none)
        {
            this.transform.GetChild(4).GetComponentInChildren<Text>().text = "你好像沒有選擇購買商品";
        }
        else if (chooseItem == BuyItem.strengthItem)
        {



            if (canBuy(10000))
            {
                player.GetComponent<Player>().MoneyChange(-10000);
                player.GetComponent<Player>().SetATK(10f);
                this.transform.GetChild(4).GetComponentInChildren<Text>().text = "謝謝惠顧";
                isBuy = true;
            }
            else {
                this.transform.GetChild(4).GetComponentInChildren<Text>().text = "喔喔~ 你好像沒有足夠的錢喔 ! ";
            }
            
            
            
        }
        else if (chooseItem == BuyItem.healthItem)
        {
            
            if (canBuy(1000)) {
                DropItemSystem.instance.AddPotionToPack(HealthPotion);
                player.GetComponent<Player>().MoneyChange(-1000);
                this.transform.GetChild(4).GetComponentInChildren<Text>().text = "謝謝惠顧";
                isBuy = true;
            }
            else {
                this.transform.GetChild(4).GetComponentInChildren<Text>().text = "喔喔~ 你好像沒有足夠的錢喔 ! ";
            }
        }
        else if (chooseItem == BuyItem.manaItem) {
            
            if (canBuy(2000))
            {
                DropItemSystem.instance.AddPotionToPack(ManaPotion);
                player.GetComponent<Player>().MoneyChange(-2000);
                this.transform.GetChild(4).GetComponentInChildren<Text>().text = "謝謝惠顧";
                isBuy = true;
            }
            else
            {
                this.transform.GetChild(4).GetComponentInChildren<Text>().text = "喔喔~ 你好像沒有足夠的錢喔 ! ";
            }
            
        }
        

        
    }
    private void clickExit() {
        if (!isBuy) { this.transform.GetChild(4).GetComponentInChildren<Text>().text = "都沒有買東西 QQ，期待下次再來 !!!"; ; }
        else { this.transform.GetChild(4).GetComponentInChildren<Text>().text = "感謝消費，希望我能在之後也能看到你 !!!"; }
        ExitShop = true;
    }
    public bool detectExitTheShop() {
        return ExitShop;
    }
    public void initShopUI() {
        this.transform.GetChild(3).GetComponentInChildren<Text>().text = "歡迎光臨，請問需要購買什麼";
        this.transform.GetChild(4).GetComponentInChildren<Text>().text = "來買來買 ! ! !";
        chooseItem = BuyItem.none;
        Buy.gameObject.SetActive(false);
        isBuy = false;
        ExitShop = false;
    }
    private bool canBuy(int cost) {
        if (player.GetComponent<Player>().GetMoney() >= cost)
        {
            return true;
        }
        else {
            return false; 
        }
    }

}
