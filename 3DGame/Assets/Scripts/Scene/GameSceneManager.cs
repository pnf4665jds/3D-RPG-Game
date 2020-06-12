using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameSceneManager : Singleton<GameSceneManager>
{
    public float BGMFadeInTime = 3;
    public float BGMFadeOutTime = 3;

    public string CurrentSceneName { get; private set; }
    public string NextSceneName { get; private set; }
    public List<Action> ActionOnCompleted { get; private set; }
    public bool IsLoadingFinish { get; private set; }    // 回傳場景是否載入完畢
    public int PassSceneNum { get; set; } = 0;   // 通過的場景數量

    private GameObject player;
    private GameObject mainCamera;
    private GameObject mainCanvas;

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
        if (CurrentSceneName != "StartGameUI")
            DoBeforeLoadScene();

        NextSceneName = nextSceneName;
        if (actions != null) {
            ActionOnCompleted = actions;
        } 
        ActionOnCompleted.AddRange(new List<Action> { SetObjectOnComplete });
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
        CurrentSceneName = scene.name;
        if(scene.name == "LoadingScene" || scene.name == "FinalScene")     // LoadingScene
        {
            player?.SetActive(false);
            mainCamera?.SetActive(false);
            mainCanvas?.SetActive(false);
        }
        else if (scene.name != "StartGameUI")
        {
            player?.SetActive(true);
            mainCamera?.SetActive(true);
            mainCanvas?.SetActive(true);
            //SoundSystem.instance.PlayBGM(BGMType.Normal);   // 淡入對應場景的BGM
        }
        IsLoadingFinish = true;
    }

    /// <summary>
    /// 設置player與mainCamera
    /// </summary>
    private void SetObjectOnComplete()
    {
        player = GameObject.FindWithTag("Player");
        mainCamera = GameObject.FindWithTag("MainCamera");
        mainCanvas = GameObject.FindWithTag("MainCanvas");
    }

    /// <summary>
    /// 離開目前場景前所做的事
    /// </summary>
    private void DoBeforeLoadScene()
    {
        MonsterSystem.instance.RemoveAllMonster();
        SoundSystem.instance.StopBGM();   // 停止對應場景的BGM
    }
}
