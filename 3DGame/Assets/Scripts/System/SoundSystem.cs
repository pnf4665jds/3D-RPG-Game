using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : Singleton<SoundSystem>
{
    public AudioSource BGMSource;
    public List<BGMs> LevelBGMList;     // 每一關使用的BGM列表
    public float FadeInTime;
    public float FadeOutTime;

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
    /// 呼叫並播放BGM
    /// </summary>
    /// <param name="type"></param>
    public void PlayBGM(BGMType type)
    {
        if (type == BGMType.None)
            return;

        AudioClip clip;
        foreach(BGMs bgms in LevelBGMList)
        {
            if(bgms.SceneName == GameSceneManager.instance.CurrentSceneName)
            {
                if (clip = bgms.GetClip(type))  // 如果有設定該種類的BGM
                    StartCoroutine(FadeBGM(clip));
                else
                    break;
            }
        }
    }

    /// <summary>
    /// 停止播放BGM
    /// </summary>
    public void StopBGM()
    {
        BGMSource.Stop();
    }

    private IEnumerator FadeBGM(AudioClip clip)
    {
        if(BGMSource.isPlaying)
            yield return SoundFadeOut(BGMSource);
        BGMSource.clip = clip;
        yield return SoundFadeIn(BGMSource);
    }

    /// <summary>
    /// 聲音淡入
    /// </summary>
    /// <param name="source"></param>
    /// <param name="fadeInTime"></param>
    /// <returns></returns>
    private IEnumerator SoundFadeIn(AudioSource source)
    {
        float timer = 0;
        source.Play();
        source.volume = 0;
        while (timer < FadeInTime && source.volume < 1)
        {
            source.volume += 1f / FadeInTime * Time.deltaTime;
            yield return null;
        }
    }

    /// <summary>
    /// 聲音淡出
    /// </summary>
    /// <param name="source"></param>
    /// <param name="fadeOutTime"></param>
    /// <returns></returns>
    private IEnumerator SoundFadeOut(AudioSource source)
    {
        float timer = 0;
        while (timer < FadeOutTime && source.volume > 0)
        {
            source.volume -= 1f / FadeOutTime * Time.deltaTime;
            yield return null;
        }
        source.Stop();
    }
}

[System.Serializable]
public class BGMs
{
    public string SceneName;    // 播放場景名
    public List<BGM> BGMList;   // 所有BGM列表

    public AudioClip GetClip(BGMType type)
    {
        foreach (BGM bgm in BGMList)
        {
            if (bgm.Type == type && bgm.BGMClip != null)
            {
                return bgm.BGMClip;
            }
        }

        return null;
    }
}

[System.Serializable]
public class BGM
{
    public BGMType Type;    // BGM類型
    public AudioClip BGMClip;   // BGM clip
}

public enum BGMType
{
    None,
    Normal,
    Boss
}
