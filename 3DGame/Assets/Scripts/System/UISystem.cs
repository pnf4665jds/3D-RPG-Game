﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : Singleton<UISystem>
{
    public GameObject PlayerPanel;
    public GameObject DialogPanel;
    public GameObject ShopPanel;
    public GameObject DeadPanel;
    public GameObject StoryPanel;
    // Start is called before the first frame update
    void Start()
    {
        AllSet(false , false , false , false , false);
    }
    private void Update()
    {
        if (GameSystem.instance.isPlayerNormal())
        {
            AllSet(true, false, false, false, false);
        }
        else if (GameSystem.instance.isPlayerTalking())
        {
            AllSet(false, true, false, false, false);
        }
        else if (GameSystem.instance.isPlayerOpenBackPack())
        {
            AllSet(true, false, false, false, false);
            PlayerPanel.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (GameSystem.instance.isPlayerShopping())
        {
            AllSet(false, false, true, false, false);
        }
        else if (GameSystem.instance.isPlayerStory())
        {
            AllSet(false, false, false, false, true);
        }
        else if (GameSystem.instance.isPlayerDead()) {
            AllSet(false, false, false, true, false);
        }

    }

    // Update is called once per frame

    private void AllSet(bool put1 , bool put2 , bool put3 , bool put4 , bool put5) {
        PlayerPanel.SetActive(put1);
        PlayerPanel.transform.GetChild(0).gameObject.SetActive(false);
        DialogPanel.SetActive(put2);
        ShopPanel.SetActive(put3);
        DeadPanel.SetActive(put4);
        StoryPanel.SetActive(put5); 

    }

    public void changeToTalkingMode(NPC character) {
        
        showDialog(character);
    }

    public void showDialog(NPC character)
    {
        UIsetNPCDetail(character);
        StartCoroutine(DialogPanel.GetComponent<Dialog>().showNPCMessage());

    }
    public void UIsetNPCDetail(NPC character) {
        setNPCName(character.getName());
        setDialog(character.getDialog());
    }
    private void setNPCName(string name)
    {
        DialogPanel.transform.GetChild(1).GetComponent<Text>().text = name;

    }
    private void setDialog(List<string> temp)
    {
        DialogPanel.GetComponent<Dialog>().setUIDialog(temp);
    }
    private void setNPCCanvas(GameObject canvas)
    {
        DialogPanel.GetComponent<Dialog>().setUICanvas(canvas);
    }
    public GameObject getPlayerDetailPanel() {
        return PlayerPanel;
    }
    public GameObject getDialogPanel() {
        return DialogPanel;
    }
    public GameObject getShopPanel() {
        return ShopPanel;
    }
    public GameObject getStoryPanel() {
        return StoryPanel;
    }
    public GameObject getDeadPanel()
    {
        return DeadPanel;
    }
}
