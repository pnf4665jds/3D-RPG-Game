using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryContoller : MonoBehaviour
{
    private GameObject player;
    public List<storyTypeBase> loadingFinishStory = new List<storyTypeBase>();
    public List<storyTypeBase> bossDeadStory = new List<storyTypeBase>();
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(LoadingFinishControl());
        StartCoroutine(BossDeadControl());
    }

    // Update is called once per frame
    private IEnumerator LoadingFinishControl() {

        yield return new WaitUntil(() => GameSceneManager.instance.IsLoadingFinish);

        foreach (storyTypeBase story in loadingFinishStory) {
            StartCoroutine(story.Play());
        }

    }
    private IEnumerator BossDeadControl()
    {
        yield return new WaitUntil(() => MonsterSystem.instance.IsBossDead); //boss死亡

        foreach (storyTypeBase story in bossDeadStory)
        {
            StartCoroutine(story.Play());
        }
        SoundSystem.instance.PlayBGM(BGMType.Normal);
    }


}
