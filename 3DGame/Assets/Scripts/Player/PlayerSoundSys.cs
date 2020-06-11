using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundSys : MonoBehaviour
{
    [SerializeField]private AudioSource PlayerSoundSystem;
    public AudioClip FirstSceneRunning_1;
    public AudioClip FirstSceneRunning_2;
    public AudioClip SecondSceneRunning_1;
    public AudioClip SecondSceneRunning_2;
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

    void FirstStepRunning()
    {
        if(GameSceneManager.instance.CurrentSceneName == "GamePlayScene")
        {
            PlayerSoundSystem.PlayOneShot(FirstSceneRunning_1,.2f);
        }
        else
        {
            PlayerSoundSystem.PlayOneShot(SecondSceneRunning_1,.2f);
        }
    }
    void SecondStepRunning()
    {
        if (GameSceneManager.instance.CurrentSceneName == "GamePlayScene")
        {
            PlayerSoundSystem.PlayOneShot(FirstSceneRunning_2, .2f);
        }
        else
        {
            PlayerSoundSystem.PlayOneShot(SecondSceneRunning_2, .2f);
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
