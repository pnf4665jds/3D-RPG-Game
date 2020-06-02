using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundSys : MonoBehaviour
{
    private AudioSource PlayerSoundSystem;
    public AudioClip FirstSceneRunning;
    public AudioClip SecondSceneRunning;
    public AudioClip AttackSound1;
    public AudioClip AttackSound2;
    public AudioClip AttackSound3;
    public AudioClip SkillSound;
    public AudioClip DeathSound;
    // Start is called before the first frame update
    void Start()
    {
        PlayerSoundSystem = GetComponentInChildren<AudioSource>();
    }

    void Running()
    {
        if(GameSceneManager.instance.CurrentSceneName == "GamePlayScene")
        {
            PlayerSoundSystem.PlayOneShot(FirstSceneRunning);
        }
        else
        {
            PlayerSoundSystem.PlayOneShot(SecondSceneRunning);
        }
    }

    void AttackFirstSound()
    {
        PlayerSoundSystem.PlayOneShot(AttackSound1);
    }
    void AttackSecond()
    {
        PlayerSoundSystem.PlayOneShot(AttackSound2);
    }
    void AttackThird()
    {
        PlayerSoundSystem.PlayOneShot(AttackSound3);
    }

    void Death()
    {
        PlayerSoundSystem.PlayOneShot(DeathSound);
    }

    public void Skill()
    {
        PlayerSoundSystem.PlayOneShot(SkillSound);
    }
}
