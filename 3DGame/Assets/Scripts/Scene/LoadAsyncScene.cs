using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadAsyncScene : MonoBehaviour
{
    public RectTransform LoadingBar;

    private string nextSceneName;
    private float progressValue;
    private Text progess;

    private AsyncOperation async = null;

    private void Start()
    {
        progess = GetComponentInChildren<Text>();
        nextSceneName = GameSceneManager.instance.NextSceneName;
        StartCoroutine(LoadScene());
    }

    // 場景讀取
    private IEnumerator LoadScene()
    {
        async = SceneManager.LoadSceneAsync(nextSceneName);
        // 防止自動跳到下個場景
        async.allowSceneActivation = false;
        // Scene載入完成時做的事
        // 語法解釋：使用Lambda Operation，傳入參數為AsyncOperation類的obj
        async.completed += (AsyncOperation obj) =>
        {
            GameSceneManager.instance.ActionOnCompleted?.ForEach(p => p.Invoke());
        };

        while(async.progress < 0.9f)
        {
            LoadingBar.localScale = new Vector3(async.progress, 1, 1);
            progess.text = (int)(LoadingBar.localScale.x * 100) + "%";
            yield return null;
        }

        // 等待讀取
        while (!async.isDone)
        {
            if (LoadingBar.localScale.x >= 0.99f)
            {
                LoadingBar.localScale = Vector3.one;
                progess.text = (int)(LoadingBar.localScale.x * 100) + "%";
                yield return new WaitForSeconds(3);
                async.allowSceneActivation = true;
            }
            else
            {
                LoadingBar.localScale += new Vector3(0.01f, 0, 0);
                progess.text = (int)(LoadingBar.localScale.x * 100) + "%";
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
}
