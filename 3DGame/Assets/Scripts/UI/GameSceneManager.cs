using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : Singleton<GameSceneManager>
{
    public string NextSceneName { get; private set; }

    public void LoadScene(string nextSceneName)
    {
        NextSceneName = nextSceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    // 測適用
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            LoadScene("GamePlayScene");
        }
    }

}
