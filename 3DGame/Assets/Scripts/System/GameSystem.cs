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
        shopping
    }

    private GameState gs;

    // Start is called before the first frame update
    void Start()
    {
        gs = GameState.normal;
    }

    public void changeModeTalking(NPC character)
    {
        gs = GameState.talking;
        changeTheWorldTime(1);
        CameraSystem.instance.showDialog(character);

    }
    public void changeModeShopping()
    {
        gs = GameState.shopping;
    }
    public void changeModeBackPack()
    {
        gs = GameState.backpack;
        changeTheWorldTime(0);

    }
    public void changeModeFollowPlayer()
    {
        gs = GameState.normal;
        changeTheWorldTime(1);

    }
    public bool isPlayerTalking()
    {
        return (gs == GameState.talking) ? true : false;
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
    public void changeTheWorldTime(float timeSpeed) {
        Time.timeScale = timeSpeed;
    }



}
