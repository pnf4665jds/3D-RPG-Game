using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : Singleton<SoundSystem>
{
    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="clip">要播放的音效</param>
    /// <param name="position">播放位置</param>
    public void PlaySound(AudioClip clip, Vector3 position)
    {
        GameObject obj = new GameObject();
        AudioSource source = obj.AddComponent<AudioSource>();
        obj.transform.position = position;
        source.clip = clip;
        source.Play();
        Destroy(obj, clip.length);
    }
}
