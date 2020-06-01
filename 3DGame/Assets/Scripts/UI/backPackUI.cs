using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class backPackUI : MonoBehaviour
{
    
    public Button healthBtn;
    public Button manaBtn;
    public Button recoveryBtn;
    private GameObject player;

   


    private void Start()
    {
       
        healthBtn.onClick.AddListener(healthBtnClick);
        manaBtn.onClick.AddListener(manaBtnClick);
        recoveryBtn.onClick.AddListener(recoveryBtnClick);
        player = GameObject.FindGameObjectWithTag("Player");

    }

    
    private void healthBtnClick() {
        
        if (!Inventory.instance.detectEmptyPotion(healthBtn.GetComponent<Image>().sprite)) {
            Inventory.instance.RemoveItem(healthBtn.GetComponent<Image>().sprite);
            player.GetComponent<Player>().Healing(20.0f);

        }

    }
    private void manaBtnClick() {
        
        if (!Inventory.instance.detectEmptyPotion(manaBtn.GetComponent<Image>().sprite))
        {
            Inventory.instance.RemoveItem(manaBtn.GetComponent<Image>().sprite);
            //player.GetComponent<Player>().;
        }
    }
    private void recoveryBtnClick() {
        
        if (!Inventory.instance.detectEmptyPotion(recoveryBtn.GetComponent<Image>().sprite))
        {
            Inventory.instance.RemoveItem(recoveryBtn.GetComponent<Image>().sprite);
            player.GetComponent<Player>().Healing(100.0f);
        }
    }

    



}
