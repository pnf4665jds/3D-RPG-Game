using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem : Singleton<GameSystem>
{

    public enum GameState
    {
        normal,
        talking,
        backpack,
        shopping,
        Animation,
        story,
        dead
    }

    private GameState gs;

    // Start is called before the first frame update
    void Start()
    {
        gs = GameState.normal;
    }
    private void Update()
    {
        print(gs);
    }
    public void changeModeTalking(NPC character)
    {
        gs = GameState.talking;
        changeTheWorldTime(1);
        UISystem.instance.changeToTalkingMode(character);


    }
    public void changeModeStory()
    {
        gs = GameState.story;
        MonsterSystem.instance.IdleAllMonsterOnAnimation();
        //changeTheWorldTime(0);

    }
    public void changeModeAnimation()
    {
        gs = GameState.Animation;
        MonsterSystem.instance.IdleAllMonsterOnAnimation();
        //changeTheWorldTime(0);


    }
    public void changeModeShopping()
    {
        gs = GameState.shopping;
    }
    public void changeModeDead()
    {
        gs = GameState.dead;
        StartCoroutine(UISystem.instance.getDeadPanel().GetComponent<deadUI>().FindPotion());
    }
    public void changeModeBackPack()
    {
        gs = GameState.backpack;
        MonsterSystem.instance.IdleAllMonsterOnAnimation();

    }
    public void changeModeFollowPlayer()
    {
        gs = GameState.normal;
        UISystem.instance.getDialogPanel().GetComponentInChildren<Dialog>().initDialog();
        UISystem.instance.getShopPanel().GetComponentInChildren<shopUI>().initShopUI();
        changeTheWorldTime(1);

    }
    public bool isPlayerTalking()
    {
        return (gs == GameState.talking) ? true : false;
    }
    public bool isAnimation()
    {
        return (gs == GameState.Animation) ? true : false;
    }
    public bool isPlayerDead()
    {
        return (gs == GameState.dead) ? true : false;
    }
    public bool isPlayerShopping()
    {
        return (gs == GameState.shopping) ? true : false;
    }
    public bool isPlayerNormal()
    {
        return (gs == GameState.normal) ? true : false;
    }
    public bool isPlayerOpenBackPack()
    {
        return (gs == GameState.backpack) ? true : false;
    }
    public bool isPlayerStory()
    {
        return (gs == GameState.story) ? true : false;
    }
    public void changeTheWorldTime(float timeSpeed) {
        Time.timeScale = timeSpeed;
    }



}
