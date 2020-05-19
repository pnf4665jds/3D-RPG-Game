using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameSceneManager : Singleton<GameSceneManager>
{
    public string NextSceneName { get; private set; }
    public List<Action> ActionOnCompleted { get; private set; }

    private GameObject player;
    private GameObject mainCamera;

    public override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary>
    /// 切換到Loading場景，Loading完成後執行actions
    /// </summary>
    /// <param name="nextSceneName"></param>
    /// <param name="actions"></param>
    public void LoadScene(string nextSceneName, List<Action> actions)
    {
        NextSceneName = nextSceneName;
        ActionOnCompleted = actions;
        SceneManager.LoadScene("LoadingScene");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "GamePlayScene")        // GamePlayScene
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
    }
}
