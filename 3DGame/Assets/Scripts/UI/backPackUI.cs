using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class backPackUI : MonoBehaviour
{
    public Button Exit;
    public Button Use;
    public Button healthBtn;
    public Button manaBtn;
    public Button recoveryBtn;

    enum potionUse {
        none ,
        health ,
        mana , 
        recovery

    }
    potionUse potion = potionUse.none;


    private void Start()
    {
       
        Use.onClick.AddListener(usePotion);
        healthBtn.onClick.AddListener(healthBtnClick);
        manaBtn.onClick.AddListener(manaBtnClick);
        recoveryBtn.onClick.AddListener(recoveryBtnClick);
        Exit.onClick.AddListener(exitBtn);
    }
    private void exitBtn() {
        this.gameObject.SetActive(false);
        GameSystem.instance.changeGameState();
    }
    private void usePotion() {
        if (potion == potionUse.health)
        {
            Debug.Log("health");
            Inventory.instance.RemoveItem(healthBtn.GetComponent<Image>().sprite);
            potion = potionUse.none;
        }
        else if (potion == potionUse.mana)
        {
            Debug.Log("mana");
            Inventory.instance.RemoveItem(manaBtn.GetComponent<Image>().sprite);
            potion = potionUse.none;
        }
        else if (potion == potionUse.recovery)
        {
            Debug.Log("recovery");
            Inventory.instance.RemoveItem(recoveryBtn.GetComponent<Image>().sprite);
            potion = potionUse.none;
        }
        else {
            Debug.Log("none");
        }
    }
    private void healthBtnClick() {
        
        if (!Inventory.instance.detectEmptyPotion(healthBtn.GetComponent<Image>().sprite)) {
            potion = potionUse.health;
        }
        else potion = potionUse.none;

    }
    private void manaBtnClick() {
        
        if (!Inventory.instance.detectEmptyPotion(manaBtn.GetComponent<Image>().sprite))
        {
            potion = potionUse.mana;
        }
        else potion = potionUse.none;
    }
    private void recoveryBtnClick() {
        
        if (!Inventory.instance.detectEmptyPotion(recoveryBtn.GetComponent<Image>().sprite))
        {
            potion = potionUse.recovery;
        }
        else potion = potionUse.none;
    }
    



}
