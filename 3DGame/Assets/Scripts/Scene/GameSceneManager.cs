using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameSceneManager : Singleton<GameSceneManager>
{
    public string NextSceneName { get; private set; }
    public List<Action> ActionOnCompleted { get; private set; }
    public bool IsLoadingFinish;    // 回傳場景是否載入完畢

    private GameObject player;
    private GameObject mainCamera;

    public override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary>
    /// 切換到Loading場景，Loading完成後執行指定actions
    /// </summary>
    /// <param name="nextSceneName"></param>
    /// <param name="actions"></param>
    public void LoadScene(string nextSceneName, List<Action> actions)
    {
        DoBeforeLoadScene();
        NextSceneName = nextSceneName;
        ActionOnCompleted = actions;
        IsLoadingFinish = false;
        SceneManager.LoadScene("LoadingScene");
    }

    /// <summary>
    /// 每當場景讀取時執行
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "GamePlayScene")        // 一開始的場景先讀取player跟main camera，目前是用GamePlayScene之後可以用MainUI
        {
            player = FindObjectOfType<Player>().gameObject;
            mainCamera = FindObjectOfType<Camera>().gameObject;
        }
        else if(scene.name == "LoadingScene")     // LoadingScene
        {
            player.SetActive(false);
            mainCamera.SetActive(false);
        }
        else
        {
            player.SetActive(true);
            mainCamera.SetActive(true);
        }
        SoundSystem.instance.PlayBGM(scene.name);   // 播放對應場景的BGM
        IsLoadingFinish = true;
    }

    /// <summary>
    /// 離開目前場景前所做的事
    /// </summary>
    private void DoBeforeLoadScene()
    {
        MonsterSystem.instance.RemoveAllMonster();
    }
}
