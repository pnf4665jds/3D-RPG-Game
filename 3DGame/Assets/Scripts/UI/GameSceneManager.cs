using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : Singleton<GameSceneManager>
{
    private LoadAsyncScene loadAsyncScene;
    private AsyncOperation op;

    public void LoadScene(string nextSceneName)
    {
        op = SceneManager.LoadSceneAsync("LoadingScene");
        StartCoroutine(Load(nextSceneName));
    }

    private IEnumerator Load(string nextSceneName)
    {
        yield return new WaitUntil(() => op.isDone);
        loadAsyncScene = FindObjectOfType<LoadAsyncScene>();
        loadAsyncScene.SetScene(nextSceneName);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            LoadScene("Maze");
        }
    }

}
