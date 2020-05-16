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
        backpack
    }

    private GameState gs;

    // Start is called before the first frame update
    void Start()
    {
        gs = GameState.normal;
    }

    public void changeModeTalking(NPC character)
    {
        CameraSystem.instance.setNPCName(character.getName());
        CameraSystem.instance.setDialog(character.getDialog());
        gs = GameState.talking;
        
        

    }
    public void changeModeBackPack()
    {

        gs = GameState.backpack;
    }
    public void changeModeFollowPlayer()
    {

        gs = GameState.normal;
        
    }
    public bool isPlayerTalking()
    {
        return (gs == GameState.talking) ? true : false;
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
