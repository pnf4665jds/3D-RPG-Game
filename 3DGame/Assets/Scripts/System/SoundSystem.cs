using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : Singleton<SoundSystem>
{
    public AudioSource BGMSource;
    public List<BGM> BGMList;     // 每一關使用的BGM列表

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="source"></param>
    /// <param name="clip"></param>
    /// <param name="delay"></param>
    /// <param name="toLoop"></param>
    public void PlaySound(AudioSource source, AudioClip clip, float volume, float delay, bool toLoop)
    {
        if (!source || !clip)
            return;

        source.clip = clip;
        source.volume = volume;
        source.loop = toLoop;
        StartCoroutine(DelayAndPlay(source, delay));
    }

    /// <summary>
    /// 經過延遲後播放
    /// </summary>
    /// <param name="source"></param>
    /// <param name="delay"></param>
    /// <returns></returns>
    private IEnumerator DelayAndPlay(AudioSource source, float delay)
    {
        yield return new WaitForSeconds(delay);
        source.Play();
    }


    ///////////////////////////////////////////////////////////////////////////////////
    // BGM
    ///////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// 播放BGM
    /// </summary>
    /// <param name="sceneName"></param>
    public void PlayBGM(float fadeInTime)
    {
        foreach(BGM bgm in BGMList)
        {
            if (bgm.SceneName == GameSceneManager.instance.CurrentSceneName && bgm.NormalBGM)
            {
                BGMSource.clip = bgm.NormalBGM;
                StartCoroutine(SoundFadeIn(BGMSource, fadeInTime));
            }
        }
    }

    /// <summary>
    /// 播放BOSS BGM
    /// </summary>
    public void PlayBossBGM(float fadeInTime)
    {
        foreach (BGM bgm in BGMList)
        {
            if (bgm.SceneName == GameSceneManager.instance.CurrentSceneName && bgm.BossBGM)
            {
                BGMSource.clip = bgm.BossBGM;
                StartCoroutine(SoundFadeIn(BGMSource, fadeInTime));
            }
        }
    }

    /// <summary>
    /// 停止播放BGM
    /// </summary>
    public void StopBGM(float fadeOutTime)
    {
        StartCoroutine(SoundFadeOut(BGMSource, fadeOutTime));
    }

    /// <summary>
    /// 聲音淡入
    /// </summary>
    /// <param name="source"></param>
    /// <param name="fadeInTime"></param>
    /// <returns></returns>
    private IEnumerator SoundFadeIn(AudioSource source, float fadeInTime)
    {
        float timer = 0;
        source.Play();
        while (timer < fadeInTime && source.volume < 1)
        {
            source.volume += 1f / fadeInTime * Time.deltaTime;
            yield return null;
        }
    }

    /// <summary>
    /// 聲音淡出
    /// </summary>
    /// <param name="source"></param>
    /// <param name="fadeOutTime"></param>
    /// <returns></returns>
    private IEnumerator SoundFadeOut(AudioSource source, float fadeOutTime)
    {
        float timer = 0;
        while (timer < fadeOutTime && source.volume > 0)
        {
            source.volume -= 1f / fadeOutTime * Time.deltaTime;
            yield return null;
        }
        source.Stop();
    }
}

[System.Serializable]
public class BGM
{
    public string SceneName;    // 播放場景名
    public AudioClip NormalBGM;      // 場景BGM
    public AudioClip BossBGM;       // Boss BGM
}
