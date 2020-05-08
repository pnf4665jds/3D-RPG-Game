using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadAsyncScene : MonoBehaviour
{
    private string nextSceneName;
    private float progressValue;
    private Text progess;
    private Slider slider;

    private AsyncOperation async = null;

    private void Start()
    {
        progess = GetComponentInChildren<Text>();
        slider = GetComponentInChildren<Slider>();
    }

    public void SetScene(string name)
    {
        nextSceneName = name;
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        async = SceneManager.LoadSceneAsync(nextSceneName);
        // 防止自動跳到下個場景
        async.allowSceneActivation = false;

        // 等待讀取
        while (!async.isDone)
        {
            if (async.progress < 0.9f)
                progressValue = async.progress;
            else
                progressValue = 1.0f;

            slider.value = progressValue;
            progess.text = (int)(slider.value * 100) + "%";
            if (progressValue >= 0.9)
                async.allowSceneActivation = true;

            yield return null;
        }
    }
}
