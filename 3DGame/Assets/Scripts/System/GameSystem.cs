using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem : Singleton<GameSystem>
{
    public GameObject backPackUI;
    public Camera playerCamera;
    public Camera backPackCamera;

    private enum GameState {
        Running,
        Stopping,
    }

    private GameState gs;

    // Start is called before the first frame update
    void Start()
    {
        gs = GameState.Running;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            changeGameState();
            //call the backPackUI
        }
    }

    public void changeGameState() {
        if (gs == GameState.Running)
        {
            Time.timeScale = 0;
            gs = GameState.Stopping;
        }
        else {
            Time.timeScale = 1 ;
            gs = GameState.Running;
        }
    }

}
