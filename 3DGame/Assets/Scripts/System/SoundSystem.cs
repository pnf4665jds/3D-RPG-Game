using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : Singleton<SoundSystem>
{
    public List<BGM> BGMList;     // 每一關使用的BGM列表

    private AudioSource bgmSource;

    public override void Awake()
    {
        base.Awake();
        CreateBGMSource();
    }

    /*/// <summary>
    /// 播放3D音效
    /// </summary>
    /// <param name="clip">要播放的音效</param>
    /// <param name="position">播放位置</param>
    /// <param name="additionalLoopTime">額外循環時間</param>
    public GameObject PlaySound3D(AudioClip clip, Vector3 position, float delay, bool toLoop)
    {
        GameObject obj = new GameObject();
        AudioSource source = obj.AddComponent<AudioSource>();
        obj.transform.position = position;
        source.clip = clip;
        source.spatialBlend = 1;
        StartCoroutine(DelayAndPlay(source, delay));
        if (!toLoop)
            Destroy(obj, clip.length + delay);
        else
            source.loop = true;

        return obj;
    }

    /// <summary>
    /// 播放2D音效
    /// </summary>
    /// <param name="clip">要播放的音效</param>
    /// <param name="position">播放位置</param>
    public GameObject PlaySound2D(AudioClip clip, float delay, bool toLoop)
    {
        GameObject obj = new GameObject();
        AudioSource source = obj.AddComponent<AudioSource>();
        source.clip = clip;
        source.spatialBlend = 0;
        StartCoroutine(DelayAndPlay(source, delay));
        if (!toLoop)
            Destroy(obj, clip.length + delay);
        else
            source.loop = true;

        return obj;
    }*/

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

    /// <summary>
    /// 播放BGM
    /// </summary>
    /// <param name="sceneName"></param>
    public void PlayBGM(string sceneName)
    {
        foreach(BGM bgm in BGMList)
        {
            if (bgm.SceneName == sceneName)
            {
                bgmSource.clip = bgm.Clip;
                bgmSource.Play();
            }
        }
    }

    /// <summary>
    /// 產生專門播放BGM的Source
    /// </summary>
    private void CreateBGMSource()
    {
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.spatialBlend = 0;
    }
}

[System.Serializable]
public class BGM
{
    public string SceneName;    // 播放場景名
    public AudioClip Clip;      // BGM
}
