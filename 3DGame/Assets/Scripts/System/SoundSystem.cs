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

    /// <summary>
    /// 播放3D音效
    /// </summary>
    /// <param name="clip">要播放的音效</param>
    /// <param name="position">播放位置</param>
    public void PlaySound3D(AudioClip clip, Vector3 position, float delay)
    {
        GameObject obj = new GameObject();
        AudioSource source = obj.AddComponent<AudioSource>();
        obj.transform.position = position;
        source.clip = clip;
        source.spatialBlend = 1;
        StartCoroutine(DelayAndPlay(source, delay));
        Destroy(obj, clip.length + delay);
    }

    /// <summary>
    /// 播放2D音效
    /// </summary>
    /// <param name="clip">要播放的音效</param>
    /// <param name="position">播放位置</param>
    public void PlaySound2D(AudioClip clip, float delay)
    {
        GameObject obj = new GameObject();
        AudioSource source = obj.AddComponent<AudioSource>();
        source.clip = clip;
        source.spatialBlend = 0;
        StartCoroutine(DelayAndPlay(source, delay));
        Destroy(obj, clip.length + delay);
    }

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
